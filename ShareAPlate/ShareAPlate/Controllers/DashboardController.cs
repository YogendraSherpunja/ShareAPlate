using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareAPlate.Models;

namespace ShareAPlate.Controllers
{
    // Controller uses Context to access data from the database
    // Controller is authorized to only allow authenticated users
    //Controller passes data to Views
    // view renders the HTML and sends it to the client
    public class DashboardController : Controller
    {
        private readonly ShareAPlateContext _context;
        public IActionResult DonateFood()
        {
            return View();
        }
        public IActionResult HomePage()
        {
            return View();
        }

        // POST: /Dashboard/DonateFood/IndividualDonation
        [HttpPost]
        public async Task<IActionResult> DonateFood(IndividualDonation model)
        {
            // Log the model state and data for debugging purposes
            System.Diagnostics.Debug.WriteLine($"FoodDetails: {model.FoodDetails}, Location: {model.Location}");

            if (ModelState.IsValid)
            {
                // Add the donation to the database
                _context.Add(model);
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("Donation added to the database.");
                // Redirect to the homepage after successful donation
                return RedirectToAction("HomePage");
            }
            else
            {
                // Add error to model state if donation is invalid
                ModelState.AddModelError("", "Invalid donation details.");
            }

            return View(model);
        }
    }
}
