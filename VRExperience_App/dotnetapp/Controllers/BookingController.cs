using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using System;
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

        // GET: Booking/ExperienceEnrollmentForm/5
        public IActionResult ExperienceEnrollmentForm(int VRExperienceID)
        {
            var experience = _context.VRExperiences.Find(VRExperienceID);
            if (experience == null)
            {
                return NotFound();
            }

            // Initialize Attendee model with VRExperienceID
            var attendee = new Attendee { VRExperienceID = VRExperienceID };
            return View(attendee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExperienceEnrollmentForm(int VRExperienceID, Attendee attendee)
        {
            Console.WriteLine("Form submitted with VRExperienceID: " + VRExperienceID);
            Console.WriteLine("Attendee data: Name = " + attendee.Name + ", Email = " + attendee.Email + ", Phone = " + attendee.PhoneNumber);

            var experience = await _context.VRExperiences
                .Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.VRExperienceID == VRExperienceID);

            if (experience == null)
            {
                Console.WriteLine("Experience not found");
                return NotFound();
            }

            if (experience.Attendees.Count >= experience.MaxCapacity)
            {
                Console.WriteLine("Maximum capacity reached");
                throw new VRExperienceBookingException("Maximum Attendees Registered");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(attendee);
            }

            // Assign the VRExperience to the attendee
            attendee.VRExperienceID = VRExperienceID;
            attendee.VRExperience = experience; // Set the VRExperience object

            // Decrease the capacity by 1
            experience.MaxCapacity -= 1;

            _context.Attendees.Add(attendee);
            _context.Update(experience); // Update the VRExperience object in the context
            await _context.SaveChangesAsync();

            Console.WriteLine("Attendee enrolled successfully");
            // Redirect to the confirmation view
            return RedirectToAction("EnrollmentConfirmation", new { AttendeeID = attendee.AttendeeID });
        }

        public async Task<IActionResult> EnrollmentConfirmation(int AttendeeID)
        {
            var attendee = await _context.Attendees
                .Include(a => a.VRExperience)
                .SingleOrDefaultAsync(a => a.AttendeeID == AttendeeID);

            if (attendee == null)
            {
                return NotFound();
            }

            return View(attendee); // Ensure this view expects an Attendee model
        }
    }
}
