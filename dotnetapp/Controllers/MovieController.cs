// using Microsoft.AspNetCore.Mvc;
// using dotnetapp.Models;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using System.Threading.Tasks;

// namespace dotnetapp.Controllers
// {
//     public class HistoricalTourController : Controller
//     {
//         private readonly ApplicationDbContext _context;

//         public HistoricalTourController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         // GET: HistoricalTour/AvailableTours
//         public async Task<IActionResult> AvailableTours(string searchString)
//         {
//             var tours = from t in _context.HistoricalTours select t;

//             if (!string.IsNullOrEmpty(searchString))
//             {
//                 tours = tours.Where(t => t.TourName.Contains(searchString));
//             }

//             return View(await tours.ToListAsync());
//         }

//         // GET: HistoricalTour/BookedTours
//         public async Task<IActionResult> BookedTours()
//         {
//             var tours = await _context.HistoricalTours.Include(t => t.Participants).Where(t => t.Participants.Any()).ToListAsync();
//             return View(tours);
//         }

//         // GET: HistoricalTour/DeleteConfirmation/5
//         public async Task<IActionResult> DeleteConfirmation(int id)
//         {
//             var tour = await _context.HistoricalTours.FindAsync(id);
//             if (tour == null)
//             {
//                 return NotFound();
//             }

//             return View(tour);
//         }

//         // POST: HistoricalTour/DeleteTour/5
//         [HttpPost, ActionName("DeleteTour")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteTourConfirmed(int id)
//         {
//             var tour = await _context.HistoricalTours.FindAsync(id);
//             if (tour == null)
//             {
//                 return NotFound();
//             }

//             _context.HistoricalTours.Remove(tour);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(AvailableTours));
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
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movie/AvailableMovies
        public async Task<IActionResult> AvailableMovies(string searchString)
        {
            var movies = from m in _context.Movies select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Title.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }

        // GET: Movie/ReviewedMovies
        public async Task<IActionResult> ReviewedMovies()
        {
            var movies = await _context.Movies.Include(m => m.Reviews).Where(m => m.Reviews.Any()).ToListAsync();
            return View(movies);
        }

        // GET: Movie/DeleteConfirmation/5
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/DeleteMovie/5
        [HttpPost, ActionName("DeleteMovie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovieConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AvailableMovies));
        }
    }
}
