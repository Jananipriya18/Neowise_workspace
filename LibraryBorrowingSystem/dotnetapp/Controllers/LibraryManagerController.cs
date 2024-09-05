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

        // GET: api/LibraryManager/Search?name=ExactName
        [HttpGet("Search")]
        public async Task<ActionResult<LibraryManager>> SearchLibraryManagerByName(string name)
        {
            var libraryManager = await _context.LibraryManagers
                .Include(lm => lm.BookLoans) // Eager load the BookLoans
                .FirstOrDefaultAsync(lm => lm.Name == name);

            if (libraryManager == null)
            {
                return NotFound();
            }

            return libraryManager;
        }

        // GET: api/LibraryManager
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryManager>>> GetLibraryManagers()
        {
            return await _context.LibraryManagers.Include(lm => lm.BookLoans).ToListAsync();
        }

        // GET: api/LibraryManager/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryManager>> GetLibraryManager(int id)
        {
            var libraryManager = await _context.LibraryManagers
                .Include(lm => lm.BookLoans)
                .FirstOrDefaultAsync(lm => lm.LibraryManagerId == id);

            if (libraryManager == null)
            {
                return NotFound();
            }

            return libraryManager;
        }
    }
}
