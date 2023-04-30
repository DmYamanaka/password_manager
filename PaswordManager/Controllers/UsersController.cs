using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.Models.User;
using PasswordManager.ServiceInterface;
using PasswordManager.Services.Cookie;
using PaswordManager.Context;
using PaswordManager.Models;
using PaswordManager.Password_hashing;
using System.Security.Claims;

namespace PaswordManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly PasswordManagerRepasitory _repository;
        private readonly IUserService _userService;
        public UsersController(
            PasswordManagerRepasitory repository,
            IUserService userService
            )
        {
            _repository = repository;
            _userService = userService;
        }

        // GET: Users
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllUsers()
        {
            return Json(await _repository.Users.Where(m => m.DeleteDate == null).ToListAsync());
        }

        // POST: Users/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var user = await _repository.Users.FirstOrDefaultAsync(m => m.Name == userLogin.Name);

            if (user == null || user?.DeleteDate!=null)
                return Json("No such user!");

            if (PasswordStorage.VerifyPassword(userLogin.Password, "sha1:64000:18:" + user?.Salt + ":" + user?.Hash))
            {
                await Cookies.SetCookie(user.Id, HttpContext);
                
                return Json("Login successful!");
            }

            return Json("Incorrect password!");
        }

        // POST: Users/Logout
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await Cookies.DeleteCookie(HttpContext);

            return Json("Logout successful");
        }

        // GET: Users/GetCurrentUser
        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (User.Claims.IsNullOrEmpty())
            {
                return Json("User ID cannot be null!");
            }
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _userService.DeteilsCurrentUser(currentUserId);

            return Json(result);
        }

        // GET: Users/Details/2
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DetailsOfUser(Guid? id)
        {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (id == null || _repository.Users == null)
            {
                return Json("User is not authorized");
            }

            var result = await _userService.DeteilsCurrentUser(currentUserId);

            return Json(result);
        }


        // POST: Users/Registration
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([Bind("Name,Passwword,EMail")] UserInput registrationUser)
        {
            var result = await _userService.Registration(HttpContext, ModelState, registrationUser);
            return Json(result);
        }

        // POST: Users/Edit
        [HttpPut]
        [Authorize]
        [Route("Edit")]
        public async Task<IActionResult> Edit(UserInput userEdit)
        {
            //TODO тут идет 2 запроса к бд, нужно подумать что с ними сделать, или так оставить
            var users = await _repository.Users.Where(m => m.DeleteDate == null && m.Id != userEdit.Id).Select(x => x.Name).ToListAsync();
            if (users.Contains(userEdit.Name))
            {
                return Json("Name already taken!");
            }
            if (users.Contains(userEdit.EMail))
            {
                return Json("Email already taken!");
            }
            var result = await _userService.Edit(userEdit);
            if (result != null)
            {
                return Json(result);
            }
            return Json("No such user!");
        }

        // POST: Users/Delete/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {

            if (_repository.Users == null)
            {
                return Problem("Entity set 'PasswordManagerRepasitory.Users'  is null.");
            }
            var user = await _repository.Users.FindAsync(id);

            if (user != null && user.DeleteDate==null)
            {
                user.DeleteDate=DateTime.Now;
                _repository.Users.Update(user);
                await _repository.SaveChangesAsync();

                await Cookies.DeleteCookie(HttpContext);

                return Json("User deleted!");
            }
            return Json(nameof(Index));
        }
    }
}
