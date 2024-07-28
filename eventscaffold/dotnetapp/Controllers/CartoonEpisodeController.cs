// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using dotnetapp.Models;
// using dotnetapp.Data;

// namespace dotnetapp.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class PlaylistController : ControllerBase
//     {
//         private readonly ApplicationDbContext _context;

//         public PlaylistController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         // GET: api/Playlist
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
//         {
//             return await _context.Playlists.ToListAsync();
//         }

//         // GET: api/Playlist/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Playlist>> GetPlaylist(int id)
//         {
//             var Playlist = await _context.Playlists.FindAsync(id);

//             if (Playlist == null)
//             {
//                 return NotFound();
//             }

//             return Playlist;
//         }

//         // POST: api/Playlist
//         [HttpPost]
//         public async Task<ActionResult<Playlist>> PostPlaylist(Playlist Playlist)
//         {
//             _context.Playlists.Add(Playlist);
//             await _context.SaveChangesAsync();

//             return CreatedAtAction("GetPlaylist", new { id = Playlist.playlistId }, Playlist);
//         }

//         // DELETE: api/Playlist/5
//         [HttpDelete("{id}")]
//         public async Task<ActionResult<Playlist>> DeletePlaylist(int id)
//         {
//             var Playlist = await _context.Playlists.FindAsync(id);
//             if (Playlist == null)
//             {
//                 return NotFound();
//             }

//             _context.Playlists.Remove(Playlist);
//             await _context.SaveChangesAsync();

//             return Playlist;
//         }

//         // GET: api/Playlist/search
//         [HttpGet("search")]
//         public async Task<ActionResult<IEnumerable<Playlist>>> SearchPlaylists([FromQuery] string searchTerm)
//         {
//             if (string.IsNullOrEmpty(searchTerm))
//             {
//                 return BadRequest("Search term cannot be empty.");
//             }

//             var lowerSearchTerm = searchTerm.ToLower();

//             var events = await _context.Playlists
//                 .Where(e => e.playlistName.ToLower().Equals(lowerSearchTerm))
//                 .ToListAsync();

//             if (!events.Any())
//             {
//                 return NotFound("The searched playlist does not exist.");
//             }

//             return events;
//         }
//     }
// }

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
    public class CartoonEpisodeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartoonEpisodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CartoonEpisode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartoonEpisode>>> GetCartoonEpisodes()
        {
            return await _context.CartoonEpisodes.ToListAsync();
        }

        // GET: api/CartoonEpisode/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartoonEpisode>> GetCartoonEpisode(int id)
        {
            var cartoonEpisode = await _context.CartoonEpisodes.FindAsync(id);

            if (cartoonEpisode == null)
            {
                return NotFound();
            }

            return cartoonEpisode;
        }

        // POST: api/CartoonEpisode
        [HttpPost]
        public async Task<ActionResult<CartoonEpisode>> PostCartoonEpisode(CartoonEpisode cartoonEpisode)
        {
            _context.CartoonEpisodes.Add(cartoonEpisode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartoonEpisode", new { id = cartoonEpisode.EpisodeId }, cartoonEpisode);
        }

        // DELETE: api/CartoonEpisode/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartoonEpisode>> DeleteCartoonEpisode(int id)
        {
            var cartoonEpisode = await _context.CartoonEpisodes.FindAsync(id);
            if (cartoonEpisode == null)
            {
                return NotFound();
            }

            _context.CartoonEpisodes.Remove(cartoonEpisode);
            await _context.SaveChangesAsync();

            return cartoonEpisode;
        }

        // GET: api/CartoonEpisode/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CartoonEpisode>>> SearchCartoonEpisodes([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var lowerSearchTerm = searchTerm.ToLower();

            var episodes = await _context.CartoonEpisodes
                .Where(e => e.CartoonSeriesName.ToLower().Contains(lowerSearchTerm)
                         || e.EpisodeTitle.ToLower().Contains(lowerSearchTerm)
                         || e.DirectorName.ToLower().Contains(lowerSearchTerm)
                         || e.Genre.ToLower().Contains(lowerSearchTerm))
                .ToListAsync();

            if (!episodes.Any())
            {
                return NotFound("No matching cartoon episodes found.");
            }

            return episodes;
        }
    }
}

