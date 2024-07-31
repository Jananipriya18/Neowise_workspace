using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    public class MovieReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieReview/ReviewForm/5
        public IActionResult ReviewForm(int MovieID)
        {
            var movie = _context.Movies.Find(MovieID);
            if (movie == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: MovieReview/ReviewForm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReviewForm(int MovieID, MovieReview movieReview)
        {
            var movie = _context.Movies.Include(m => m.Reviews).FirstOrDefault(m => m.MovieID == MovieID);
            if (movie == null)
            {
                return NotFound();
            }

            movieReview.MovieID = MovieID;

            if (!ModelState.IsValid)
            {
                return View(movieReview);
            }

            // Example condition for throwing a custom exception
            if (movieReview.Rating < 1 || movieReview.Rating > 5)
            {
                throw new MovieReviewException("The rating must be between 1 and 5.");
            }

            _context.MovieReviews.Add(movieReview);
            _context.SaveChanges();

            // Redirect to ReviewConfirmation action
            return RedirectToAction("ReviewConfirmation", new { reviewId = movieReview.MovieReviewID });
        }

        // GET: MovieReview/ReviewConfirmation/5
        public IActionResult ReviewConfirmation(int reviewId)
        {
            var movieReview = _context.MovieReviews.Include(mr => mr.Movie).SingleOrDefault(mr => mr.MovieReviewID == reviewId);
            if (movieReview == null)
            {
                return NotFound();
            }

            return View(movieReview);
        }
    }
}