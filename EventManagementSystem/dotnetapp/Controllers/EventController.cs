using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Exceptions; // Make sure to include this for EventDateException
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Event
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event eventModel)
        {
            if (eventModel == null)
            {
                return BadRequest("Event cannot be null.");
            }

            // Validate EventDate
            if (DateTime.TryParse(eventModel.EventDate, out var eventDate))
            {
                if (eventDate < DateTime.Today)
                {
                    throw new EventDateException("Event Date is a past date.");
                }
            }
            else
            {
                return BadRequest("Invalid date format.");
            }

            _context.Events.Add(eventModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = eventModel.EventId }, eventModel);
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.Include(e => e.Attendees).ToListAsync();
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eventModel = await _context.Events
                .Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (eventModel == null)
            {
                return NotFound();
            }

            return eventModel;
        }
    }
}
