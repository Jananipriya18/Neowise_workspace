// using Microsoft.AspNetCore.Mvc;
// using dotnetapp.Models;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using System.Threading.Tasks;

// namespace dotnetapp.Controllers
// {
//     public class ClassController : Controller
//     {
//         private readonly ApplicationDbContext _context;

//         public ClassController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         // GET: Class/AvailableClasses
//         public async Task<IActionResult> AvailableClasses(string searchString)
//         {
//             var classes = from c in _context.Classes select c;

//             if (!string.IsNullOrEmpty(searchString))
//             {
//                 classes = classes.Where(c => c.ClassName.Contains(searchString));
//             }

          

//             return View(await classes.ToListAsync());
//         }

//         // GET: Class/BookedClasses
//         public async Task<IActionResult> BookedClasses()
//         {
//             var classes = await _context.Classes.Include(c => c.Students).Where(c => c.Students.Any()).ToListAsync();
//             return View(classes);
//         }

//         // GET: Class/DeleteConfirmation/5
//         public async Task<IActionResult> DeleteConfirmation(int id)
//         {
//             var classEntity = await _context.Classes.FindAsync(id);
//             if (classEntity == null)
//             {
//                 return NotFound();
//             }

//             return View(classEntity);
//         }

//         // POST: Class/DeleteClass/5
//         [HttpPost, ActionName("DeleteClass")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteClassConfirmed(int id)
//         {
//             var classEntity = await _context.Classes.FindAsync(id);
//             if (classEntity == null)
//             {
//                 return NotFound();
//             }

//             _context.Classes.Remove(classEntity);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(AvailableClasses));
//         }
//     }
// }

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
