using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MusicController(AppDbContext context)
        {
            _context = context;
        }

        // Implement a method to display songs associated with a library card.
        [HttpGet("Playlist/{playlistId}/Songs")]
        public IActionResult DisplaySongsForPlaylist(int playlistId)
        {
            Console.WriteLine(playlistId);
            var playlist = _context.Playlists.FirstOrDefault(lc => lc.Id == playlistId);

            if (playlist == null)
            {
                return NotFound(); // Handle the case where the library card with the given ID doesn't exist
            }

            var songs = _context.Songs
                .Where(b => b.PlaylistId == playlistId)
                .ToList();

            return Ok(songs);
        }

        // Implement a method to add a song.
        [HttpPost("AddSong")]
        public IActionResult AddSong([FromBody] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Songs.Add(song);
                _context.SaveChanges();
                return CreatedAtAction(nameof(DisplaySongsForPlaylist), new { id = song.Id }, song);
            }
            return BadRequest(ModelState); // Return the model validation errors
        }

        // Implement a method to display all songs in the library.
        [HttpGet("Songs")]
        public IActionResult DisplayAllSongs()
        {
            var songs = _context.Songs.ToList();
            return Ok(songs);
        }

        // Method to search for songs by title
        [HttpGet("SearchSongsByTitle")]
        public IActionResult SearchSongsByTitle([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                var allSongs = _context.Songs.ToList();
                return Ok(allSongs);
            }

            var songs = _context.Songs
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(songs);
        }
    }
}
