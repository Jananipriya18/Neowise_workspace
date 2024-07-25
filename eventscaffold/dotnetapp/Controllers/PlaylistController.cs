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
    public class PlaylistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaylistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Playlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            return await _context.Playlists.ToListAsync();
        }

        // GET: api/Playlist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var Playlist = await _context.Playlists.FindAsync(id);

            if (Playlist == null)
            {
                return NotFound();
            }

            return Playlist;
        }

        // POST: api/Playlist
        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist Playlist)
        {
            _context.Playlists.Add(Playlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaylist", new { id = Playlist.playlistId }, Playlist);
        }

        // DELETE: api/Playlist/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Playlist>> DeletePlaylist(int id)
        {
            var Playlist = await _context.Playlists.FindAsync(id);
            if (Playlist == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(Playlist);
            await _context.SaveChangesAsync();

            return Playlist;
        }

        // GET: api/Playlist/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Playlist>>> SearchPlaylists([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var lowerSearchTerm = searchTerm.ToLower();

            var events = await _context.Playlists
                .Where(e => e.playlistName.ToLower().Equals(lowerSearchTerm))
                .ToListAsync();

            if (!events.Any())
            {
                return NotFound("The searched playlist does not exist.");
            }

            return events;
        }
    }
}
