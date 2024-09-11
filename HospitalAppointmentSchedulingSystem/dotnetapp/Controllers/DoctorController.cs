using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Exceptions;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Doctor
        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor([FromBody] Doctor doctor)
        {
            // Validate DoctorFee
            if (doctor.DoctorFee <= 0)
            {
                throw new PriceException("DoctorFee must be greater than 0");
            }

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorId }, doctor);
        }

        // GET: api/Doctor
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        // {
        //     return await _context.Doctors.Include(d => d.Appointments).ToListAsync();
        // }

        // GET: api/Doctor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.DoctorId == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        [HttpGet("sortedByFee")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsSortedByFee()
        {
            var doctors = await _context.Doctors
                .OrderByDescending(d => d.DoctorFee) 
                .Include(d => d.Appointments) 
                .ToListAsync();

            return Ok(doctors);
        }
    }
}
