using Microsoft.AspNetCore.Mvc;
using ShareAPlate.Models;

namespace ShareAPlate.Controllers
{
    public class DonorController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public DonorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // **CREATE Operation**
        // GET: Donor/Create - Displays a form for adding a new donor
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donor/Create - Adds a new donor to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DonorModel donor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Donors.Add(donor);
                    _context.SaveChanges();
                    return RedirectToAction("List"); // Redirect to list after adding
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving data: " + ex.Message);
                }
            }
            return View(donor);
        }

        // **READ Operation**
        // GET: Donor/List - Displays a list of all donors
        public IActionResult List()
        {
            var donors = _context.Donors.ToList();
            return View(donors);
        }

        // GET: Donor/Details/{id} - Displays details of a specific donor
        public IActionResult Details(int id)
        {
            var donor = _context.Donors.FirstOrDefault(d => d.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }

        // **UPDATE Operation**
        // GET: Donor/Edit/{id} - Displays a form to edit a donor's details
        public IActionResult Edit(int id)
        {
            var donor = _context.Donors.FirstOrDefault(d => d.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }

        // POST: Donor/Edit/{id} - Updates the donor's details in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DonorModel updatedDonor)
        {
            if (id != updatedDonor.DonorId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Donors.Update(updatedDonor);
                    _context.SaveChanges();
                    return RedirectToAction("List"); // Redirect to list after updating
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating data: " + ex.Message);
                }
            }
            return View(updatedDonor);
        }

        // **DELETE Operation**
        // GET: Donor/Delete/{id} - Displays a confirmation page for deletion
        public IActionResult Delete(int id)
        {
            var donor = _context.Donors.FirstOrDefault(d => d.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }

        // POST: Donor/Delete/{id} - Deletes a donor from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var donor = _context.Donors.FirstOrDefault(d => d.DonorId == id);
            if (donor != null)
            {
                try
                {
                    _context.Donors.Remove(donor);
                    _context.SaveChanges();
                    return RedirectToAction("List"); // Redirect to list after deletion
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error deleting data: " + ex.Message);
                }
            }
            return RedirectToAction("List");
        }
    }
}
