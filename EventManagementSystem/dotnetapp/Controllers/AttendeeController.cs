using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Attendee
        [HttpPost]
        public async Task<IActionResult> CreateAttendee([FromBody] Attendee attendee)
        {
            if (attendee == null)
            {
                return BadRequest("Attendee cannot be null.");
            }

            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            // Retrieve the attendee with event details
            var createdAttendee = await _context.Attendees
                .Include(a => a.Event)  // Eager load the Event
                .FirstOrDefaultAsync(a => a.AttendeeId == attendee.AttendeeId);

            return CreatedAtAction(nameof(GetAttendee), new { id = createdAttendee.AttendeeId }, createdAttendee);
        }

        // DELETE: api/Attendee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendee(int id)
        {
            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null)
            {
                return NotFound();
            }

            _context.Attendees.Remove(attendee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Attendee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendee>>> GetAttendees()
        {
            return await _context.Attendees.Include(a => a.Event).ToListAsync();
        }

        // GET: api/Attendee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendee>> GetAttendee(int id)
        {
            var attendee = await _context.Attendees
                .Include(a => a.Event)
                .FirstOrDefaultAsync(a => a.AttendeeId == id);

            if (attendee == null)
            {
                return NotFound();
            }

            return attendee;
        }
    }
}
