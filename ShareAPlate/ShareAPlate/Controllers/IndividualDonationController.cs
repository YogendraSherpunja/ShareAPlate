using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareAPlate.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShareAPlate.Controllers
{
    public class IndividualDonationController : Controller
    {
        private readonly ShareAPlateContext _context;

        public IndividualDonationController(ShareAPlateContext context)
        {
            _context = context;
        }

        // GET: IndividualDonation
        public async Task<IActionResult> Index()
        {
            var donations = await _context.IndividualDonations.Include(d => d.User).ToListAsync();
            return View(donations);
        }

        // GET: IndividualDonation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IndividualDonation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IndividualDonation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        // GET: IndividualDonation/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var donation = await _context.IndividualDonations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // POST: IndividualDonation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IndividualDonation donation)
        {
            if (id != donation.DonorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndividualDonationExists(donation.DonorId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        // GET: IndividualDonation/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.IndividualDonations
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonorId == id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // POST: IndividualDonation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.IndividualDonations.FindAsync(id);
            _context.IndividualDonations.Remove(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndividualDonationExists(int id)
        {
            return _context.IndividualDonations.Any(e => e.DonorId == id);
        }
    }
}
