using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking/TourEnrollmentForm/5
        public IActionResult TourEnrollmentForm(int HistoricalTourID)
        {
            var tour = _context.HistoricalTours.Find(HistoricalTourID);
            if (tour == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Booking/TourEnrollmentForm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TourEnrollmentForm(int HistoricalTourID, Participant participant)
        {
            var tour = _context.HistoricalTours.Include(t => t.Participants).FirstOrDefault(t => t.HistoricalTourID == HistoricalTourID);
            if (tour == null)
            {
                return NotFound();
            }

            participant.HistoricalTourID = HistoricalTourID;

            if (tour.Participants.Count >= tour.Capacity)
            {
                throw new HistoricalTourBookingException("Maximum Number Reached");
            }

            if (!ModelState.IsValid)
            {
                return View(participant);
            }

            _context.Participants.Add(participant);
            tour.Capacity -= 1; // Reduce the capacity
            _context.SaveChanges();

            // Redirect to EnrollmentConfirmation action
            return RedirectToAction("EnrollmentConfirmation", new { participantId = participant.ParticipantID });
        }

        // GET: Booking/EnrollmentConfirmation/5
        public IActionResult EnrollmentConfirmation(int participantId)
        {
            var participant = _context.Participants.Include(p => p.HistoricalTour).SingleOrDefault(p => p.ParticipantID == participantId);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }
    }
}
