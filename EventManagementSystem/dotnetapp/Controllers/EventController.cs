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
        private static readonly List<string> AllowedLocations = new List<string> { "Dallas", "Los Angeles" };

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

            // Validate the Location
            if (!AllowedLocations.Contains(eventModel.Location))
            {
                throw new EventLocationException($"Location '{eventModel.Location}' is not allowed. Only allowed locations are: {string.Join(", ", AllowedLocations)}.");
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
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Event>> GetEvent(int id)
        // {
        //     var eventModel = await _context.Events
        //         .Include(e => e.Attendees)
        //         .FirstOrDefaultAsync(e => e.EventId == id);

        //     if (eventModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return eventModel;
        // }


        //GET: api/Event/SortedByNameDesc
        [HttpGet("SortedByNameDesc")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsSortedByNameDesc()
        {
            // Retrieve all events and sort them by Name in descending order
            var sortedEvents = await _context.Events
                                             .OrderByDescending(e => e.Name)
                                             .Include(e => e.Attendees)
                                             .ToListAsync();
            return Ok(sortedEvents);
        }
    }
}
