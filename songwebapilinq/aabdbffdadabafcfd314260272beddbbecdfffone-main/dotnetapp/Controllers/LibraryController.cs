using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibraryController(AppDbContext context)
        {
            _context = context;
        }

        // Implement a method to display books associated with a library card.
        [HttpGet("Playlist/{libraryCardId}/Songs")]
        public IActionResult DisplaySongsForPlaylist(int libraryCardId)
        {
            Console.WriteLine(libraryCardId);
            var libraryCard = _context.Playlists.FirstOrDefault(lc => lc.Id == libraryCardId);

            if (libraryCard == null)
            {
                return NotFound(); // Handle the case where the library card with the given ID doesn't exist
            }

            var books = _context.Songs
                .Where(b => b.PlaylistId == libraryCardId)
                .ToList();

            return Ok(books);
        }

        // Implement a method to add a book.
        [HttpPost("AddSong")]
        public IActionResult AddSong([FromBody] Song book)
        {
            if (ModelState.IsValid)
            {
                _context.Songs.Add(book);
                _context.SaveChanges();
                return CreatedAtAction(nameof(DisplaySongsForPlaylist), new { libraryCardId = book.PlaylistId }, book);
            }
            return BadRequest(ModelState); // Return the model validation errors
        }

        // Implement a method to display all books in the library.
        [HttpGet("Songs")]
        public IActionResult DisplayAllSongs()
        {
            var books = _context.Songs.ToList();
            return Ok(books);
        }

        // Method to search for books by title
        [HttpGet("SearchSongsByTitle")]
        public IActionResult SearchSongsByTitle([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                var allSongs = _context.Songs.ToList();
                return Ok(allSongs);
            }

            var filteredSongs = _context.Songs
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(filteredSongs);
        }

        // Implement a method to get available books.
        [HttpGet("AvailableSongs")]
        public IActionResult GetAvailableSongs()
        {
            var availableSongs = _context.Songs
                .Where(b => b.PlaylistId == null) // Songs that are not borrowed
                .ToList();

            return Ok(availableSongs);
        }

        // Implement a method to get borrowed books.
        [HttpGet("BorrowedSongs")]
        public IActionResult GetBorrowedSongs()
        {
            var borrowedSongs = _context.Songs
                .Where(b => b.PlaylistId != null) // Songs that are borrowed
                .ToList();

            return Ok(borrowedSongs);
        }
    }
}
