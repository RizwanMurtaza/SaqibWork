using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serials.Core;
using System.Linq;

namespace Serials.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOptions<List<SiteUser>> _users;

        public AccountController(IOptions<List<SiteUser>> users)
        {
            _users = users;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View(new UserToLoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserToLoginViewModel userToLogin)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Account/Login.cshtml", userToLogin);

            var user = _users.Value.FirstOrDefault(x => x.UserName.ToLower() == userToLogin.UserName?.ToLower() && x.Password == userToLogin.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.FullName),
                    new Claim("FullName", user.UserName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                    RedirectUri = "/Home",

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Redirect("/Home");
            }

            ModelState.AddModelError("Error", "Invalid username or password");
            return View("~/Views/Account/Login.cshtml", userToLogin);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}