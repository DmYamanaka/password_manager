using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using PasswordManager.Models.User;
using PasswordManager.ServiceInterface;
using PasswordManager.Services.Cookie;
using PaswordManager.Context;
using PaswordManager.Password_hashing;
using System.Linq;
using System.Security.Claims;

namespace PasswordManager.Services.UserService
{
    public class UserService : IUserService
    {
        PasswordManagerRepasitory _passwordManagerRepasitory;
        public UserService(PasswordManagerRepasitory passwordManagerRepasitory)
        {
            _passwordManagerRepasitory = passwordManagerRepasitory;
        }

        public async Task<string> Registration(HttpContext httpContext, ModelStateDictionary modelState, UserInput registrationUser)
        {
            var user = new User
            {
                Name = registrationUser.Name,
                EMail = registrationUser.EMail
            };
            var _user = await _passwordManagerRepasitory.Users.FirstOrDefaultAsync(x => x.Name == user.Name || x.EMail==user.EMail);
            if (_user != null)
            {
                return "Name or email already taken!";
            }
            if (!registrationUser.Password.IsNullOrEmpty())
            {
                var password_hash = PasswordStorage.CreateHash(registrationUser.Password);
                string[] split = password_hash.Split(":");
                if (modelState.IsValid)
                {
                    user.Id = Guid.NewGuid();
                    user.CreateDate = DateTime.Now;
                    user.EditDate = null;
                    user.DeleteDate = null;
                    user.Salt = split[3];
                    user.Hash = split[4];
                    _passwordManagerRepasitory.Add(user);
                    await _passwordManagerRepasitory.SaveChangesAsync();

                    await Cookies.SetCookie(user.Id, httpContext);

                    return "Registration successful!";
                }
            }

            return "Incorrect incoming parameters!";
        }
        public async Task<UserInput> Edit(UserInput userEdit)
        {
            var _user = await _passwordManagerRepasitory.Users.FirstOrDefaultAsync(x => x.Id == userEdit.Id || x.EMail == userEdit.EMail);
            if (_user != null || _user.DeleteDate == null)
            {
                _user.Name = userEdit.Name;
                _user.Phone = userEdit.Phone;
                _user.Photo = userEdit.Photo;
                _user.EMail = userEdit.EMail;
                _user.Hash = _user.Hash;
                _user.Salt = _user.Salt;
                if (!userEdit.Password.IsNullOrEmpty())
                {
                    var password_hash = PasswordStorage.CreateHash(userEdit.Password);
                    string[] split = password_hash.Split(":");
                    _user.Salt = split[3];
                    _user.Hash = split[4];
                }
                _user.DeleteDate = null;
                _user.EditDate = DateTime.Now;
                _passwordManagerRepasitory.Update(_user);
                await _passwordManagerRepasitory.SaveChangesAsync();
                return userEdit;
            }

            return null;
        }
        public async Task<UserInput> DeteilsCurrentUser(Guid id)
        {
            var user = await _passwordManagerRepasitory.Users.FirstOrDefaultAsync(x => x.Id == id);
            var userResult = new UserInput {
                Id = user.Id,
                Password = string.Empty,
                Name = user.Name, 
                EMail = user.EMail, 
                Photo = user.Photo
                
            };
            if (user == null || user.DeleteDate != null)
            {
                return null;
            }
            return userResult;
        }
    }
}
