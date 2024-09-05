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

        // GET: api/Student/Search?name=ExactName
        [HttpGet("Search")]
        public async Task<ActionResult<Student>> SearchStudentByName(string name)
        {
            var student = await _context.Students.Include(a => a.Courses).FirstOrDefaultAsync(a => a.Name == name);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
    }
}
