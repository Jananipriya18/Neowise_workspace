using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Exceptions; // Add this using directive for the exception
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SpiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Spice
        [HttpPost]
        public async Task<IActionResult> CreateSpice([FromBody] Spice spice)
        {
            if (spice == null)
            {
                return BadRequest("Spice cannot be null.");
            }

            // Check for valid StockQuantity
            if (spice.StockQuantity <= 0)
            {
                throw new StockQuantityException("Stock quantity must be a positive value.");
            }

            _context.Spices.Add(spice);
            await _context.SaveChangesAsync();

            // Retrieve the spice with customer details
            var createdSpice = await _context.Spices
                .Include(s => s.Customer)  // Eager load the Customer
                .FirstOrDefaultAsync(s => s.SpiceId == spice.SpiceId);

            return CreatedAtAction(nameof(CreateSpice), new { id = createdSpice.SpiceId }, createdSpice);
        }

        // GET: api/Spice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spice>>> GetSpices()
        {
            return await _context.Spices.Include(s => s.Customer).ToListAsync();
        }

        // PUT: api/Spice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpice(int id, [FromBody] Spice spice)
        {
            if (id != spice.SpiceId)
            {
                return BadRequest("Spice ID mismatch.");
            }

            var existingSpice = await _context.Spices.FindAsync(id);
            if (existingSpice == null)
            {
                return NotFound();
            }

            // Check for valid StockQuantity
            if (spice.StockQuantity <= 0)
            {
                throw new StockQuantityException("Stock quantity must be a positive value.");
            }

            // Update fields
            existingSpice.Name = spice.Name;
            existingSpice.OriginCountry = spice.OriginCountry;
            existingSpice.FlavorProfile = spice.FlavorProfile;
            existingSpice.StockQuantity = spice.StockQuantity;
            existingSpice.CustomerId = spice.CustomerId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the item no longer exists, return NotFound
                return NotFound();
            }

            // Retrieve the updated spice with customer details
            var updatedSpice = await _context.Spices
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(s => s.SpiceId == id);

            return Ok(updatedSpice);
        }
    }
}
