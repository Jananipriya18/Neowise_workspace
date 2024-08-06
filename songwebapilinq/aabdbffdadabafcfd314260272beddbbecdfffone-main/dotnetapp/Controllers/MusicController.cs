using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("Playlist/{playlistId}/Songs")]
        public IActionResult DisplaySongsForPlaylist(int playlistId)
        {
            var playlist = _context.Playlists
                .Include(p => p.Songs) // Include songs
                .FirstOrDefault(p => p.Id == playlistId);

            if (playlist == null)
            {
                return NotFound(); // Handle the case where the playlist with the given ID doesn't exist
            }

            var result = new
            {
                Playlist = new
                {
                    playlist.Id,
                    playlist.Name,
                    playlist.Description
                },
                Songs = playlist.Songs.Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.Artist,
                    s.ReleaseYear
                }).ToList()
            };

            return Ok(result);
        }


        // Implement a method to add a song.
        [HttpPost("AddSong")]
        public IActionResult AddSong([FromBody] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Songs.Add(song);
                _context.SaveChanges();

                var songWithPlaylist = _context.Songs
                    .Include(s => s.Playlist) // Include the playlist details
                    .Where(s => s.Id == song.Id)
                    .Select(s => new
                    {
                        s.Id,
                        s.Title,
                        s.Artist,
                        s.ReleaseYear,
                        Playlist = s.Playlist != null ? new
                        {
                            s.Playlist.Id,
                            s.Playlist.Name,
                            s.Playlist.Description
                        } : null
                    })
                    .FirstOrDefault();

                return CreatedAtAction(nameof(DisplayAllSongs), new { id = song.Id }, songWithPlaylist);
            }
            return BadRequest(ModelState); // Return the model validation errors
        }


        // Implement a method to display all songs.
       [HttpGet("Songs")]
        public IActionResult DisplayAllSongs()
        {
            var songs = _context.Songs
                .Include(s => s.Playlist) // Include the playlist details
                .Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.Artist,
                    s.ReleaseYear,
                    Playlist = s.Playlist != null ? new
                    {
                        s.Playlist.Id,
                        s.Playlist.Name,
                        s.Playlist.Description
                    } : null
                })
                .ToList();

            return Ok(songs);
        }


        // Implement a method to search for songs by title.
        [HttpGet("SearchSongsByTitle")]
        public IActionResult SearchSongsByTitle([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                var allSongs = _context.Songs
                    .Include(s => s.Playlist) // Include playlist details
                    .Select(s => new
                    {
                        s.Id,
                        s.Title,
                        s.Artist,
                        s.ReleaseYear,
                        Playlist = s.Playlist != null ? new
                        {
                            s.Playlist.Id,
                            s.Playlist.Name,
                            s.Playlist.Description
                        } : null
                    })
                    .ToList();

                return Ok(allSongs);
            }

            var songs = _context.Songs
                .Include(s => s.Playlist) // Include playlist details
                .Where(s => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.Artist,
                    s.ReleaseYear,
                    Playlist = s.Playlist != null ? new
                    {
                        s.Playlist.Id,
                        s.Playlist.Name,
                        s.Playlist.Description
                    } : null
                })
                .ToList();

            return Ok(songs);
        }

    }
}
