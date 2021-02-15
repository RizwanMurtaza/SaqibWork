using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Serials.Mvc.Razor.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOptions<List<UserToLogin>> _users;
        public AccountController(IOptions<List<UserToLogin>> users)
        {

            _users = users;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View(new UserToLogin());
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserToLogin userToLogin)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Account/Login.cshtml", userToLogin);

            var user = userToLogin.UserName == "Admin" && userToLogin.Password == "Admin1";

            if (user)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userToLogin.UserName),
                    new Claim("FullName", userToLogin.UserName),
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
    }
}