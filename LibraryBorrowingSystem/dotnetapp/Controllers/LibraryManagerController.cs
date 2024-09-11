using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibraryManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/LibraryManager
        [HttpPost]
        public async Task<ActionResult<LibraryManager>> CreateLibraryManager([FromBody] LibraryManager libraryManager)
        {
            _context.LibraryManagers.Add(libraryManager);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(SearchLibraryManagerByName), new { name = libraryManager.Name }, libraryManager);
        }

        // // GET: api/LibraryManager/Search?name=ExactName
        // [HttpGet("Search")]
        // public async Task<ActionResult<LibraryManager>> SearchLibraryManagerByName(string name)
        // {
        //     var libraryManager = await _context.LibraryManagers
        //         .Include(lm => lm.BookLoans) // Eager load the BookLoans
        //         .FirstOrDefaultAsync(lm => lm.Name == name);

        //     if (libraryManager == null)
        //     {
        //         return NotFound();
        //     }

        //     return libraryManager;
        // }

        // GET: api/LibraryManager/Search?name=PartialName
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<LibraryManager>>> SearchLibraryManagerByName(string name)
        {
            // Ensure the provided name is valid and has at least 1 character
            if (string.IsNullOrEmpty(name) || name.Length < 1)
            {
                return BadRequest("Name must contain at least 1 character.");
            }

            // Get the last character from the name to perform the search
            var lastChar = name[^1].ToString();

            // Perform a search where the library manager's name ends with the given last character
            var libraryManagers = await _context.LibraryManagers
                .Include(lm => lm.BookLoans) // Eager load the BookLoans
                .Where(lm => lm.Name.EndsWith(lastChar))
                .ToListAsync();

            // Return 404 if no library managers are found
            if (libraryManagers == null || libraryManagers.Count == 0)
            {
                return NotFound();
            }

            return Ok(libraryManagers);
        }
    }
}
