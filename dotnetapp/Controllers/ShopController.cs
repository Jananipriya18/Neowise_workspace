using dotnetapp.Exceptions;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("/")]
    public class ShopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /getAllShopitem
        [HttpGet("getAllShopitem")]
        public async Task<ActionResult<IEnumerable<Shop>>> GetAllShopItems()
        {
            try
            {
                var shopItems = await _context.Shops.ToListAsync();
                return Ok(shopItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST /addShopitem
        [HttpPost("addShopitem")]
        public async Task<ActionResult<Shop>> AddShopItem(Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (shop.Price < 0)
                {
                    throw new PriceItemException("Price cannot be less than 0.");
                }

                _context.Shops.Add(shop);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAllShopItems), new { id = shop.Id }, shop);
            }
            catch (PriceItemException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
