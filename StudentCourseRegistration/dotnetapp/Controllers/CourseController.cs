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
            if (course == null)
            {
                return BadRequest("Course cannot be null.");
            }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var createdCourse = await _context.Courses
                .Include(b => b.Student)  // Eager load the Student
                .FirstOrDefaultAsync(b => b.CourseId == book.CourseId);

            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.CourseId }, createdCourse);
        }


        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var book = await _context.Courses.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.Include(b => b.Student).ToListAsync();
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var book = await _context.Courses.Include(b => b.Student).FirstOrDefaultAsync(b => b.CourseId == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
    }
}

