using Microsoft.AspNetCore.Mvc;
using ShareAPlate.Models;
using Microsoft.EntityFrameworkCore;

namespace ShareAPlate.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ShareAPlateContext _context;

        public ProfileController(ShareAPlateContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}


        // Action to show the Edit Profile page
        public IActionResult Index()
        {
            // Ensure the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
            }

            // Retrieve the currently logged-in user
            var currentUserEmail = User.Identity.Name; // Assuming email is used as the identity name
            var user = _context.Users.FirstOrDefault(u => u.Email == currentUserEmail);

            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not found
            }

            return View(user); // Pass the user object to the view
        }


        // Action to handle the form submission and update the user's profile
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(User model)
        {
            if (ModelState.IsValid)
            {
                // Ensure the user is authenticated
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
                }

                var currentUserEmail = User.Identity.Name;
                var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);

                if (userToUpdate == null)
                {
                    return NotFound(); // User not found, handle appropriately
                }

                // Update user information
                userToUpdate.UserFirstName = model.UserFirstName;
                userToUpdate.UserLastName = model.UserLastName;
                userToUpdate.Email = model.Email;
                userToUpdate.Location = model.Location;
                userToUpdate.Number = model.Number;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Redirect back to profile page after update
            }

            return View("Edit", model); // Return edit view with validation errors if any
        }
    }
}
