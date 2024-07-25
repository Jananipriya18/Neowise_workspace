using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using dotnetapp.Data;

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

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var Event = await _context.Events.FindAsync(id);

            if (Event == null)
            {
                return NotFound();
            }

            return Event;
        }

        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event Event)
        {
            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = Event.playlistId }, Event);
        }

        // DELETE: api/Event/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var Event = await _context.Events.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(Event);
            await _context.SaveChangesAsync();

            return Event;
        }

        // GET: api/Event/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Event>>> SearchEvents([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var lowerSearchTerm = searchTerm.ToLower();

            var events = await _context.Events
                .Where(e => e.playlistName.ToLower().Contains(lowerSearchTerm))
                .ToListAsync();

            return events;
        }
    }
}
