using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly FreelancerProjectDbContext _context;

        public ProjectController(FreelancerProjectDbContext context)
        {
            _context = context;
        }

        // Retrieve all projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _context.Projects
                                         .Include(p => p.Freelancer) // Include related Freelancer data
                                         .ToListAsync();
            return Ok(projects);
        }

        // Add a new project
        [HttpPost]
        public async Task<ActionResult> AddProject(Project project)
        {
            // Check if the freelancer exists
            var freelancer = await _context.Freelancers.FindAsync(project.FreelancerId);
            if (freelancer == null)
            {
                return BadRequest("Invalid Freelancer ID");
            }

            // Assign the fetched Freelancer entity to the Project
            project.Freelancer = freelancer;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllProjects), new { id = project.ProjectID }, project);
        }

        // Delete a project by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id <= 0) // Check for invalid ID
            {
                return BadRequest("Not a valid Project id");
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
