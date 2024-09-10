using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Exceptions;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null.");
            }

            // Check if a book with the same title already exists
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
            if (existingBook != null)
            {
                // Throw the custom exception when a duplicate title is found
                throw new BookNameException("A book with the same title already exists.");
            }

            // Add the new book if no duplicate is found
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Retrieve the book with author details
            var createdBook = await _context.Books
                .Include(b => b.Author)  // Eager load the Author
                .FirstOrDefaultAsync(b => b.BookId == book.BookId);

            return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookId }, createdBook);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
    }
}
