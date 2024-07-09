using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly JobApplicationDbContext _context;

        public ApplicationController(JobApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetAllApplications()
        {
            var applications = await _context.Applications.ToListAsync();
            return Ok(applications);
        }

        [HttpPost]
        public async Task<ActionResult> AddApplication(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid Application id");
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
