using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using dotnetapp.Exceptions;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order cannot be null.");
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Retrieve the order with customer details
            var createdOrder = await _context.Orders
                .Include(o => o.Customer)  // Eager load the Customer
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            return CreatedAtAction(nameof(GetOrders), new { id = createdOrder.OrderId }, createdOrder);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("Order ID mismatch.");
            }

            if (!_context.Orders.Any(o => o.OrderId == id))
            {
                return NotFound("Order not found.");
            }

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.Include(o => o.Customer).ToListAsync();
        }
    }
}
