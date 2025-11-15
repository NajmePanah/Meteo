using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Main.Meteo.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Main.Meteo.ExtensionMethods;

namespace Main.Meteo.Controllers
{
    
    public class AuthController : Controller
    {
        private readonly MeteoDbContext _db;

        public AuthController(MeteoDbContext db) => _db = db;

        [HttpGet]
        public IActionResult Login(string? returnUrl = null) =>
            View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _db.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == model.UserName);
            var test = model.Password.ToSha256Hash();
            if (user == null || model.Password.ToSha256Hash() != user.H256Password)
            {
                this.SetMessage(MessageEnum.danger, "نام کاربری یا کلمه عبور اشتباه است.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id.ToString())
            };

            foreach (var role in user.UserRoles.Select(ur => ur.Role))
                claims.Add(new Claim(ClaimTypes.Role, role.Title));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties { IsPersistent = model.RememberMe });

            return Redirect(model.ReturnUrl ?? "/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
