using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCenterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceCenterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/ServiceCenter
        [HttpPost]
        public async Task<ActionResult<ServiceCenter>> CreateServiceCenter([FromBody] ServiceCenter serviceCenter)
        {
            _context.ServiceCenters.Add(serviceCenter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateServiceCenter), new { id = serviceCenter.ServiceCenterId }, serviceCenter); // 201 Created
        }

        // GET: api/ServiceCenter/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<ServiceCenter>>> SearchServiceCenterByName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 1)
            {
                return BadRequest("Name must contain at least 1 character."); // 400 Bad Request
            }

            var serviceCenters = await _context.ServiceCenters
                .Where(sc => sc.Name.EndsWith(name)).ToListAsync();

            if (serviceCenters.Count == 0) return NotFound(); // 404 Not Found
            return Ok(serviceCenters); // 200 OK
        }
    }
}
