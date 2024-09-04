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
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByTitlePrefix(string prefix)
        {
            var courses = await _context.Courses
                .Where(c => c.Title.StartsWith(prefix))
                .Include(c => c.Student)
                .ToListAsync();

            if (courses == null || !courses.Any())
            {
                return NotFound();
            }

            return courses;
        }
    }
}
