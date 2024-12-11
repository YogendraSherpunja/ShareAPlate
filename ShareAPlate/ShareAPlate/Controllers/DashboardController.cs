using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareAPlate.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ShareAPlate.Controllers
{
    [Authorize] // Ensure all actions are accessible only by authenticated users
    public class DashboardController : Controller
    {
        private readonly ShareAPlateContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor to inject ShareAPlateContext and IHttpContextAccessor
        public DashboardController(ShareAPlateContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        // Renders the Donate Food page
        public IActionResult DonateFood()
        {
            return View();
        }

        // Renders the Home Page
        public IActionResult HomePage()
        {
            return View();
        }

        // Handles form submission for Individual Donations
        [HttpPost]
        public async Task<IActionResult> DonateFood(IndividualDonation model)
        {
            System.Diagnostics.Debug.WriteLine($"FoodDetails: {model.FoodDetails}, Location: {model.Location}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.IndividualDonations.Add(model); // Ensure your DbSet name matches
                    await _context.SaveChangesAsync();

                    System.Diagnostics.Debug.WriteLine("Donation successfully added to the database.");

                    TempData["SuccessMessage"] = "Donation submitted successfully!";
                    return RedirectToAction("HomePage");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error saving donation: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while saving your donation. Please try again.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please correct the highlighted errors and try again.");
            }

            return View(model);
        }

        // GET: Dashboard/UpdateProfile
        public async Task<IActionResult> UpdateProfile()
        {
            // Get the logged-in user's ID (you can retrieve it from the session or cookies)
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name); // Adjust based on your setup

            // Fetch the user's details from the database
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound(); // User not found
            }

            return View(user); // Pass user data to the view
        }

        // POST: Dashboard/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Find the existing user record
                    var existingUser = await _context.Users.FindAsync(user.UserId);
                    if (existingUser == null)
                    {
                        return NotFound(); // User not found
                    }

                    // Update user properties
                    existingUser.UserFirstName = user.UserFirstName;
                    existingUser.UserLastName = user.UserLastName;
                    existingUser.Email = user.Email;
                    existingUser.Location = user.Location;
                    existingUser.Number = user.Number;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction("HomePage", "Dashboard");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while updating the profile: {ex.Message}");
                }
            }

            return View(user); // Re-render the form if validation fails
        }
    }
}
