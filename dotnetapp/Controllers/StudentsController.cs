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
            var shopItems = await _context.Shops.ToListAsync();
            return Ok(shopItems);
        }

        // POST /addShopitem
        [HttpPost("addShopitem")]
        public async Task<ActionResult<Shop>> AddShopItem(Shop shop)
        {
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllShopItems), new { id = shop.Id }, shop);
        }
    }
}
