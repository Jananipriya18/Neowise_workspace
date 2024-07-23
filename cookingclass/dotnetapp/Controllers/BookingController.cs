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
        public IActionResult ClassEnrollmentForm(int classId, Student student)  // Ensure the parameter name is classId
        {
            var classEntity = _context.Classes.Include(c => c.Students).FirstOrDefault(c => c.ClassID == classId);
            if (classEntity == null)
            {
                return NotFound();
            }

            student.ClassID = classId;

            if (classEntity.Students.Count >= classEntity.Capacity)
            {
                throw new CookingClassBookingException("Maximum Number Reached");
            }

            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _context.Students.Add(student);
            classEntity.Capacity -= 1; // Reduce the capacity
            _context.SaveChanges();

            // Redirect to EnrollmentConfirmation action
            return RedirectToAction("EnrollmentConfirmation", new { studentId = student.StudentID });
        }

        // GET: Booking/EnrollmentConfirmation/5
        public IActionResult EnrollmentConfirmation(int studentId)
        {
            var student = _context.Students.Include(s => s.Class).SingleOrDefault(s => s.StudentID == studentId);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
    }
}
