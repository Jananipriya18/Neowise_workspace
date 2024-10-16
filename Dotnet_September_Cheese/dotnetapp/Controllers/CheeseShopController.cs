using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using dotnetapp.Data;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheeseShopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CheeseShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CheeseShop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheeseShop>>> GetCheeseShops()
        {
            return await _context.CheeseShops.ToListAsync();
        }

        // GET: api/CheeseShop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheeseShop>> GetCheeseShop(int id)
        {
            var cheeseShop = await _context.CheeseShops.FindAsync(id);

            if (cheeseShop == null)
            {
                return NotFound();
            }

            return cheeseShop;
        }

        // POST: api/CheeseShop
        [HttpPost]
        public async Task<ActionResult<CheeseShop>> PostCheeseShop(CheeseShop cheeseShop)
        {
            _context.CheeseShops.Add(cheeseShop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheeseShop", new { id = cheeseShop.shopId }, cheeseShop);
        }

        // DELETE: api/CheeseShop/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CheeseShop>> DeleteCheeseShop(int id)
        {
            var cheeseShop = await _context.CheeseShops.FindAsync(id);
            if (cheeseShop == null)
            {
                return NotFound();
            }

            _context.CheeseShops.Remove(cheeseShop);
            await _context.SaveChangesAsync();

            return cheeseShop;
        }

        // GET: api/CheeseShop/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CheeseShop>>> SearchCheeseShops([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm) || searchTerm.Length != 1)
            {
                return BadRequest("Search term must be a single character.");
            }

            var lowerSearchTerm = searchTerm.ToLower();

            var shops = await _context.CheeseShops
                .Where(s => s.ownerName.ToLower().EndsWith(lowerSearchTerm))
                .ToListAsync();

            if (!shops.Any())
            {
                return NotFound("No matching cheese shops found.");
            }

            return Ok(shops);
        }
    }
}
