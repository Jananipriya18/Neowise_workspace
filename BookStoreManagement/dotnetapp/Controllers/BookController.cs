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
        // [HttpPost]
        // public async Task<IActionResult> PostBook([FromBody] Book book)
        // {
        //     if (book == null)
        //     {
        //         return BadRequest("Book cannot be null.");
        //     }

        //     // Validate the price of the book
        //     if (book.Price <= 0)
        //     {
        //         // Throw the custom PriceException if the price is 0 or negative
        //         throw new PriceException("Price cannot be 0 or negative.");
        //     }

        //     // Add the new book if no issues are found
        //     _context.Books.Add(book);
        //     await _context.SaveChangesAsync();

        //     // Retrieve the book with author details
        //     var createdBook = await _context.Books
        //         .Include(b => b.Author)  // Eager load the Author
        //         .FirstOrDefaultAsync(b => b.BookId == book.BookId);

        //     return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookId }, createdBook);
        // }

        [HttpPost]
public async Task<IActionResult> PostBook([FromBody] Book book)
{
    if (book == null)
    {
        return BadRequest("Book cannot be null.");
    }

    // Validate the price of the book
    if (book.Price <= 0)
    {
        // Throw the custom PriceException if the price is 0 or negative
        throw new PriceException("Price cannot be 0 or negative.");
    }

    // Add the new book if no issues are found
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
