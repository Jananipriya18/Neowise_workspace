using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    public class HistoricalTourController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoricalTourController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HistoricalTour/AvailableTours
        public async Task<IActionResult> AvailableTours(string searchString)
        {
            var tours = from t in _context.HistoricalTours select t;

            if (!string.IsNullOrEmpty(searchString))
            {
                tours = tours.Where(t => t.TourName.Contains(searchString));
            }

            return View(await tours.ToListAsync());
        }

        // GET: HistoricalTour/BookedTours
        public async Task<IActionResult> BookedTours()
        {
            var tours = await _context.HistoricalTours.Include(t => t.Participants).Where(t => t.Participants.Any()).ToListAsync();
            return View(tours);
        }

        // GET: HistoricalTour/DeleteConfirmation/5
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var tour = await _context.HistoricalTours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: HistoricalTour/DeleteTour/5
        [HttpPost, ActionName("DeleteTour")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTourConfirmed(int id)
        {
            var tour = await _context.HistoricalTours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            _context.HistoricalTours.Remove(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AvailableTours));
        }
    }
}
