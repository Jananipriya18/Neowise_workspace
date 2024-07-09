using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerController : ControllerBase
    {
        private readonly FreelancerProjectDbContext _context;

        public FreelancerController(FreelancerProjectDbContext context)
        {
            _context = context;
        }

        // Retrieve all freelancers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Freelancer>>> GetAllFreelancers()
        {
            var freelancers = await _context.Freelancers
                                             .Include(f => f.Projects) // Include related Projects data
                                             .ToListAsync();
            return Ok(freelancers);
        }

        // Retrieve all freelancer names
        [HttpGet("FreelancerNames")]
        public async Task<ActionResult<IEnumerable<string>>> GetFreelancerNames()
        {
            var names = await _context.Freelancers.Select(f => f.FreelancerName).ToListAsync();
            return Ok(names);
        }

        // Add a new freelancer
        [HttpPost]
        public async Task<ActionResult> AddFreelancer(Freelancer freelancer)
        {
            if (freelancer == null)
            {
                return BadRequest();
            }

            _context.Freelancers.Add(freelancer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllFreelancers), new { id = freelancer.FreelancerID }, freelancer);
        }

        // Delete a freelancer by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFreelancer(int id)
        {
            if (id <= 0) // Check for invalid ID
            {
                return BadRequest("Not a valid Freelancer id");
            }

            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer == null)
            {
                return NotFound();
            }

            _context.Freelancers.Remove(freelancer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
