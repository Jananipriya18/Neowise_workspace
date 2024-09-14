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
    public class AttendeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Attendee
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

    // Simply return a 201 status with the createdAttendee data, without linking a GET endpoint
    return StatusCode(201, createdAttendee);
}

// GET: api/Attendee
[HttpGet]
public async Task<ActionResult<IEnumerable<Attendee>>> GetAttendees()
{
    var attendees = await _context.Attendees
        .Include(a => a.Event)  // Eager load the Event details
        .ToListAsync();

    if (attendees == null || attendees.Count == 0)
    {
        return NotFound("No attendees found.");
    }

    return Ok(attendees);
}



        // PUT: api/Attendee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendee(int id, [FromBody] Attendee attendee)
        {
            if (id != attendee.AttendeeId)
            {
                return BadRequest("Attendee ID mismatch.");
            }

            _context.Entry(attendee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool AttendeeExists(int id)
        {
            return _context.Attendees.Any(e => e.AttendeeId == id);
        }
    }
}
