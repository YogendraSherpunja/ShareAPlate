using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareAPlate.Models;

namespace ShareAPlate.Controllers
{
   // [Authorize] // Ensure all actions are accessible only by authenticated users
    public class DashboardController : Controller
    {
        private readonly ShareAPlateContext _context;

        // Constructor to inject ShareAPlateContext
        public DashboardController(ShareAPlateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Renders the Donate Food page.
        /// </summary>
        public IActionResult DonateFood()
        {
            return View();
        }

        /// <summary>
        /// Renders the Home Page.
        /// </summary>
        public IActionResult HomePage()
        {
            return View();
        }

        /// <summary>
        /// Handles form submission for Individual Donations.
        /// </summary>
        /// <param name="model">The donation details submitted by the user.</param>
        /// <returns>Redirects to HomePage or redisplays the form if validation fails.</returns>
        [HttpPost]
        public async Task<IActionResult> DonateFood(IndividualDonation model)
        {
            // Log model details for debugging purposes
            System.Diagnostics.Debug.WriteLine($"FoodDetails: {model.FoodDetails}, Location: {model.Location}");

            if (ModelState.IsValid)
            {
                try
                {
                    // Add the donation record to the database
                    _context.IndividualDonations.Add(model); // Ensure your DbSet name matches
                    await _context.SaveChangesAsync();

                    // Log success message
                    System.Diagnostics.Debug.WriteLine("Donation successfully added to the database.");

                    // Redirect to the Home Page after successful donation
                    TempData["SuccessMessage"] = "Donation submitted successfully!";
                    return RedirectToAction("HomePage");
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    System.Diagnostics.Debug.WriteLine($"Error saving donation: {ex.Message}");

                    // Add error message to the model state
                    ModelState.AddModelError("", "An error occurred while saving your donation. Please try again.");
                }
            }
            else
            {
                // Add general error message to ModelState
                ModelState.AddModelError("", "Please correct the highlighted errors and try again.");
            }

            // If we reach here, re-render the form with the entered data
            return View(model);
        }
    }
}
