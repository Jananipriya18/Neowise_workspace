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
    public class JobController : ControllerBase
    {
        private readonly JobApplicationDbContext _context;

        public JobController(JobApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return Ok(jobs);
        }

        [HttpGet("titles")]
        public async Task<ActionResult<IEnumerable<string>>> GetJobTitles()
        {
            var jobTitles = await _context.Jobs.Select(j => j.JobTitle).ToListAsync();
            return Ok(jobTitles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobById(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult> AddJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJobById), new { id = job.JobID }, job);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid Job ID");
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
