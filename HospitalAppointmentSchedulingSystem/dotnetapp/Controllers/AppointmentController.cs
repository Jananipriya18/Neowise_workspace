using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Appointment cannot be null.");
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Retrieve the appointment with doctor details
            var createdAppointment = await _context.Appointments
                .Include(a => a.Doctor)  // Eager load the Doctor
                .FirstOrDefaultAsync(a => a.AppointmentId == appointment.AppointmentId);

            return CreatedAtAction(nameof(CreateAppointment), new { id = createdAppointment.AppointmentId }, createdAppointment);
        }

        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.Include(a => a.Doctor).ToListAsync();
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            var existingAppointment = await _context.Appointments.FindAsync(id);
            if (existingAppointment == null)
            {
                return NotFound();
            }

            // Update fields
            existingAppointment.AppointmentDate = appointment.AppointmentDate;
            existingAppointment.PatientName = appointment.PatientName;
            existingAppointment.Reason = appointment.Reason;
            existingAppointment.DoctorId = appointment.DoctorId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the item no longer exists, return NotFound
                return NotFound();
            }

            // Retrieve the updated appointment with doctor details
            var updatedAppointment = await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            return Ok(updatedAppointment);
        }
    }
}
