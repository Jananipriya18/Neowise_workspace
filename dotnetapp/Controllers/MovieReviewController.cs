// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using dotnetapp.Exceptions;
// using dotnetapp.Models;
// using System.Linq;
// using System.Threading.Tasks;

// namespace dotnetapp.Controllers
// {
//     public class BookingController : Controller
//     {
//         private readonly ApplicationDbContext _context;

//         public BookingController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         // GET: Booking/TourEnrollmentForm/5
//         public IActionResult TourEnrollmentForm(int HistoricalTourID)
//         {
//             var tour = _context.HistoricalTours.Find(HistoricalTourID);
//             if (tour == null)
//             {
//                 return NotFound();
//             }

//             return View();
//         }

//         // POST: Booking/TourEnrollmentForm
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult TourEnrollmentForm(int HistoricalTourID, Participant participant)
//         {
//             var tour = _context.HistoricalTours.Include(t => t.Participants).FirstOrDefault(t => t.HistoricalTourID == HistoricalTourID);
//             if (tour == null)
//             {
//                 return NotFound();
//             }

//             participant.HistoricalTourID = HistoricalTourID;

//             if (tour.Participants.Count >= tour.Capacity)
//             {
//                 throw new HistoricalTourBookingException("Maximum Number Reached");
//             }

//             if (!ModelState.IsValid)
//             {
//                 return View(participant);
//             }

//             _context.Participants.Add(participant);
//             tour.Capacity -= 1; // Reduce the capacity
//             _context.SaveChanges();

//             // Redirect to EnrollmentConfirmation action
//             return RedirectToAction("EnrollmentConfirmation", new { participantId = participant.ParticipantID });
//         }

//         // GET: Booking/EnrollmentConfirmation/5
//         public IActionResult EnrollmentConfirmation(int participantId)
//         {
//             var participant = _context.Participants.Include(p => p.HistoricalTour).SingleOrDefault(p => p.ParticipantID == participantId);
//             if (participant == null)
//             {
//                 return NotFound();
//             }

//             return View(participant);
//         }
//     }
// }


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
