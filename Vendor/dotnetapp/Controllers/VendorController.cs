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
    public class VendorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VendorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vendor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var Vendor = await _context.Vendors.FindAsync(id);

            if (Vendor == null)
            {
                return NotFound();
            }

            return Vendor;
        }

        // POST: api/Vendor
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor Vendor)
        {
            _context.Vendors.Add(Vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = Vendor.vendorId }, Vendor);
        }

        // DELETE: api/Vendor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendor>> DeleteVendor(int id)
        {
            var Vendor = await _context.Vendors.FindAsync(id);
            if (Vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(Vendor);
            await _context.SaveChangesAsync();

            return Vendor;
        }

        // GET: api/Vendor/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Vendor>>> SearchVendors([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var lowerSearchTerm = searchTerm.ToLower();

            var events = await _context.Vendors
                .Where(e => e.name.ToLower().Equals(lowerSearchTerm))
                .ToListAsync();

            if (!events.Any())
            {
                return NotFound("The searched Vendor does not exist.");
            }

            return events;
        }
    }
}
