using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(SearchAuthorByName), new { name = author.Name }, author);
        }

        // GET: api/Author/Search?name=ExactName
        [HttpGet("Search")]
        public async Task<ActionResult<Author>> SearchAuthorByName(string name)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Name == name);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }
    }
}
