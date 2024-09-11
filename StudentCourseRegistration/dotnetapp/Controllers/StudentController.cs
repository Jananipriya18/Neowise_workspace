using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(SearchStudentByName), new { name = student.Name }, student);
        }

        // GET: api/Student/Search?name=PartialName
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchStudentByName(string name)
        {
            // Ensure we check if 'name' has at least 3 characters.
            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                return BadRequest("Name must be at least 3 characters long.");
            }

            // Get the first 3 characters from the name to perform a partial search
            var prefix = name.Substring(0, 3);

            // Perform a search where the student's name starts with the given prefix
            var students = await _context.Students
                .Include(a => a.Courses)
                .Where(a => a.Name.StartsWith(prefix))
                .ToListAsync();

            if (students == null || students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

    }
}
