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
        [HttpGet("Customer/{libraryCardId}/Books")]
        public IActionResult DisplayBooksForCustomer(int libraryCardId)
        {
            Console.WriteLine(libraryCardId);
            var libraryCard = _context.Customers.FirstOrDefault(lc => lc.Id == libraryCardId);

            if (libraryCard == null)
            {
                return NotFound(); // Handle the case where the library card with the given ID doesn't exist
            }

            var books = _context.Books
                .Where(b => b.CustomerId == libraryCardId)
                .ToList();

            return Ok(books);
        }

        // Implement a method to add a book.
        [HttpPost("AddBook")]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return CreatedAtAction(nameof(DisplayBooksForCustomer), new { libraryCardId = book.CustomerId }, book);
            }
            return BadRequest(ModelState); // Return the model validation errors
        }

        // Implement a method to display all books in the library.
        [HttpGet("Books")]
        public IActionResult DisplayAllBooks()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }

        // Method to search for books by title
        [HttpGet("SearchBooksByTitle")]
        public IActionResult SearchBooksByTitle([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                var allBooks = _context.Books.ToList();
                return Ok(allBooks);
            }

            var filteredBooks = _context.Books
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(filteredBooks);
        }

        // Implement a method to get available books.
        [HttpGet("AvailableBooks")]
        public IActionResult GetAvailableBooks()
        {
            var availableBooks = _context.Books
                .Where(b => b.CustomerId == null) // Books that are not borrowed
                .ToList();

            return Ok(availableBooks);
        }

        // Implement a method to get borrowed books.
        [HttpGet("BorrowedBooks")]
        public IActionResult GetBorrowedBooks()
        {
            var borrowedBooks = _context.Books
                .Where(b => b.CustomerId != null) // Books that are borrowed
                .ToList();

            return Ok(borrowedBooks);
        }
    }
}
