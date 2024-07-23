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

        // GET: Booking/ClassEnrollmentForm/5
        public IActionResult ClassEnrollmentForm(int classId)  // Ensure the parameter name is classId
        {
            var classEntity = _context.Classes.Find(classId);
            if (classEntity == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Booking/ClassEnrollmentForm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClassEnrollmentForm(int classId, Attendee student)  // Ensure the parameter name is classId
        {
            var classEntity = _context.Classes.Include(c => c.Attendees).FirstOrDefault(c => c.VRExperienceID == classId);
            if (classEntity == null)
            {
                return NotFound();
            }

            student.VRExperienceID = classId;

            if (classEntity.Attendees.Count >= classEntity.MaxCapacity)
            {
                throw new VRExperienceBookingException("Maximum Number Reached");
            }

            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _context.Attendees.Add(student);
            classEntity.MaxCapacity -= 1; // Reduce the capacity
            _context.SaveChanges();

            // Redirect to EnrollmentConfirmation action
            return RedirectToAction("EnrollmentConfirmation", new { studentId = student.AttendeeID });
        }

        // GET: Booking/EnrollmentConfirmation/5
        public IActionResult EnrollmentConfirmation(int studentId)
        {
            var student = _context.Attendees.Include(s => s.Class).SingleOrDefault(s => s.AttendeeID == studentId);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
    }
}
