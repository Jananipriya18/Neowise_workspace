using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
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
            var applications = await _context.Applications
                                             .Include(a => a.Job) // Include related Job data
                                             .ToListAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplicationById(int id)
        {
            var application = await _context.Applications
                                            .Include(a => a.Job) // Include related Job data
                                            .FirstOrDefaultAsync(a => a.ApplicationID == id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        [HttpPost]
        public async Task<ActionResult> AddApplication(Application application)
        {
            // Fetch the Job entity using the JobID provided in the Application
            var job = await _context.Jobs.FindAsync(application.JobID);
            if (job == null)
            {
                return BadRequest("Invalid Job ID");
            }

            // Assign the fetched Job entity to the Application
            application.Job = job;

            // Reset JobID to avoid EF trying to insert a new Job
            application.JobID = job.JobID;

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetApplicationById), new { id = application.ApplicationID }, application);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid Application ID");
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
