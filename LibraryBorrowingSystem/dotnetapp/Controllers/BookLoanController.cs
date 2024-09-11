using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookLoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/BookLoan
        [HttpPost]
        public async Task<IActionResult> CreateBookLoan([FromBody] BookLoan bookLoan)
        {
            _context.BookLoans.Add(bookLoan);
            await _context.SaveChangesAsync();

            // Retrieve the loan with library manager details
            var createdLoan = await _context.BookLoans
                .Include(bl => bl.LibraryManager)  // Eager load the LibraryManager
                .FirstOrDefaultAsync(bl => bl.BookLoanId == bookLoan.BookLoanId);

            return CreatedAtAction(nameof(GetBookLoan), new { id = createdLoan.BookLoanId }, createdLoan);
        }

        // DELETE: api/BookLoan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookLoan(int id)
        {
            var bookLoan = await _context.BookLoans.FindAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            _context.BookLoans.Remove(bookLoan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/BookLoan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookLoan>>> GetBookLoans()
        {
            return await _context.BookLoans.Include(bl => bl.LibraryManager).ToListAsync();
        }

        // GET: api/BookLoan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookLoan>> GetBookLoan(int id)
        {
            var bookLoan = await _context.BookLoans.Include(bl => bl.LibraryManager).FirstOrDefaultAsync(bl => bl.BookLoanId == id);

            if (bookLoan == null)
            {
                return NotFound();
            }

            return bookLoan;
        }
    }
}
