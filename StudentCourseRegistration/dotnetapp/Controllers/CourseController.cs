using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

       // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course created successfully" });
        }


        // GET: api/Course/search/{prefix}
        [HttpGet("search/{prefix}")]
        public async Task<ActionResult<Course>> GetCoursesByTitlePrefix(string prefix)
        {
            var course = await _context.Courses
                .Include(c => c.Student) // Ensure that Student data is included
                .FirstOrDefaultAsync(c => c.Title == prefix); // Use 'prefix' here

            if (course == null)
            {
                return NotFound(new { message = "No course found with the exact title." });
            }

            return Ok(course);
        }
    }
}

