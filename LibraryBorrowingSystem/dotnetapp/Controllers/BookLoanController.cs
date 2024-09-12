using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Exceptions;

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


        [HttpPost]
        public async Task<IActionResult> CreateBookLoan([FromBody] BookLoan bookLoan)
        {
            // Validate if the LoanAmount is at least 1
            if (bookLoan.LoanAmount < 1)
            {
                // Throw a custom exception if the LoanAmount is invalid
                throw new dotnetapp.Exceptions.BookLoanException("Loan Amount must be at least 1.");
            }

            // Add the book loan to the database
            _context.BookLoans.Add(bookLoan);
            await _context.SaveChangesAsync();

            // Retrieve the loan with library manager details
            var createdLoan = await _context.BookLoans
                .Include(bl => bl.LibraryManager)  // Eager load the LibraryManager
                .FirstOrDefaultAsync(bl => bl.BookLoanId == bookLoan.BookLoanId);

            // Return a response with the created loan
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
