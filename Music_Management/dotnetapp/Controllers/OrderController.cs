using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetOrders()
        {
            // Retrieve all orders including related MusicRecords
            var orders = await _context.Orders
                .Include(o => o.MusicRecords) // Include related MusicRecords
                .ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return NoContent(); // Return 204 No Content if no records are found
            }

            // Project the results to avoid cyclic references
            var orderResults = orders.Select(o => new
            {
                o.OrderId,
                o.CustomerName,
                o.OrderDate,
                MusicRecords = o.MusicRecords.Select(mr => new
                {
                    mr.MusicRecordId,
                    mr.Artist,
                    mr.Album,
                    mr.Genre,
                    mr.Price,
                    mr.StockQuantity
                }).ToList()
            }).ToList();

            return Ok(orderResults); // Return 200 OK with the list of Orders
        }



        [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetOrder(int id)
    {
        // Retrieve a single order by ID including related MusicRecords
        var order = await _context.Orders
            .Include(o => o.MusicRecords) // Include related MusicRecords
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null)
        {
            return NotFound(); // Return 404 Not Found if the order does not exist
        }

        // Project the result to avoid cyclic references
        var orderResult = new
        {
            order.OrderId,
            order.CustomerName,
            order.OrderDate,
            MusicRecords = order.MusicRecords.Select(mr => new
            {
                mr.MusicRecordId,
                mr.Artist,
                mr.Album,
                mr.Genre,
                mr.Price,
                mr.StockQuantity
            }).ToList()
        };

        return Ok(orderResult); // Return 200 OK with the order details
    }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            // Add a new order to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.MusicRecords) // Include related MusicRecords
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.OrderId == id);
        }
    }
}
