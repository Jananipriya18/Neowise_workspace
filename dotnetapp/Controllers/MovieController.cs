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
                movies = movies.Where(m => m.Title.EndsWith(searchString));
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

