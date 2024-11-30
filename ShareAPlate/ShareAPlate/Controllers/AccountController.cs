using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareAPlate.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

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
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(User model)
        {
            // Log the model state and data for debugging purposes
            System.Diagnostics.Debug.WriteLine($"Email: {model.Email}, Password: {model.Password}");

            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                // If the user doesn't exist, add them to the database
                if (existingUser == null)
                {
                    //// Hash the password before saving
                    //var passwordHasher = new PasswordHasher<User>();
                    //model.Password = passwordHasher.HashPassword(model, model.Password); // Store the hashed password

                    // Add the user to the database
                    _context.Add(model);
                    await _context.SaveChangesAsync();

                    // Redirect to the login page after successful registration
                    return RedirectToAction("Login");
                }
                else
                {
                    // Add error to model state if user exists
                    ModelState.AddModelError("", "Username or Email already exists.");
                }
            }
            else
            {
                // Log errors in model state for debugging
                var errors = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
                System.Diagnostics.Debug.WriteLine("Model state errors: " + errors);
            }

            // Return the view if the model state is invalid
            return View(model);
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
                //var passwordHasher = new PasswordHasher<User>();
                ////var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);
                //// Hash and store the password
                //string hashedPassword = passwordHasher.HashPassword(user, "userPlainTextPassword");
                //user.Password = hashedPassword;

                //// Verify the password
                //PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.Password, "userInputPassword");

                //if (result == PasswordVerificationResult.Success)
                //{
                    // Set the session variable
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    Console.WriteLine("User logged in");

                    // Authenticate and sign in the user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, email),
                         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                     };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirect to the original page if returnUrl is set
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                //}
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
