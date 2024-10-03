using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceBookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiceBooking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceBooking>>> GetServiceBookings()
        {
            var serviceBookings = await _context.ServiceBookings.Include(sb => sb.ServiceCenter).ToListAsync();
            if (serviceBookings.Count == 0) return NoContent(); // 204 No Content
            return Ok(serviceBookings); // 200 OK
        }

        // GET: api/ServiceBooking/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceBooking>> GetServiceBooking(int id)
        {
            var serviceBooking = await _context.ServiceBookings.Include(sb => sb.ServiceCenter)
                .FirstOrDefaultAsync(sb => sb.ServiceBookingId == id);
            if (serviceBooking == null) return NotFound(); // 404 Not Found
            return Ok(serviceBooking); // 200 OK
        }

        // POST: api/ServiceBooking
        [HttpPost]
        public async Task<ActionResult<ServiceBooking>> CreateServiceBooking([FromBody] ServiceBooking serviceBooking)
        {
            if (serviceBooking.ServiceCost < 1)
            {
                throw new ServiceBookingException("Service Cost must be at least 1.");
            }

            _context.ServiceBookings.Add(serviceBooking);
            await _context.SaveChangesAsync();

            // Eagerly load the ServiceCenter after saving the new ServiceBooking
            var createdBooking = await _context.ServiceBookings
                .Include(sb => sb.ServiceCenter)  // Include the ServiceCenter details
                .FirstOrDefaultAsync(sb => sb.ServiceBookingId == serviceBooking.ServiceBookingId);

            return CreatedAtAction(nameof(GetServiceBooking), new { id = createdBooking.ServiceBookingId }, createdBooking);
        }


        // DELETE: api/ServiceBooking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceBooking(int id)
        {
            var serviceBooking = await _context.ServiceBookings.FindAsync(id);
            if (serviceBooking == null) return NotFound(); // 404 Not Found

            _context.ServiceBookings.Remove(serviceBooking);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content
        }
    }
}
