using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareAPlate.Models;
using System.Security.Claims;

namespace ShareAPlate.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ShareAPlateContext _context;

        public AccountController(ShareAPlateContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(); // Render the login view
        }

        // POST: /Account/Login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            System.Diagnostics.Debug.WriteLine($"Username: {email}, Password: {password}");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                // set the session variable
                HttpContext.Session.SetInt32("UserId", user.Id);
                Console.WriteLine("user logged in");
                // Authenticate and sign in the user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    // Redirect to the original page
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();

        }
        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
