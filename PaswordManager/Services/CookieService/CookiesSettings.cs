using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PaswordManager.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Services.Cookie
{
    public class Cookies : ControllerBase
    {
        public static async Task SetCookie(Guid id, HttpContext httpContext)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, id.ToString().ToLower()) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public static async Task DeleteCookie(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
