using dotnetapp.Exceptions;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Exceptions;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("/")]
    public class PantryItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PantryItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /getAllPantryitem
        [HttpGet("getAllPantryitem")]
        public async Task<ActionResult<IEnumerable<PantryItem>>> GetAllPantryItems()
        {
            try
            {
                var pantryItems = await _context.PantryItems.ToListAsync();
                return Ok(pantryItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST /addPantryitem
        [HttpPost("addPantryitem")]
        public async Task<ActionResult<PantryItem>> AddPantryItem(PantryItem pantryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (pantryItem.StockItem <= 0)
                {
                    throw new StockItemException("Stock Item must be greater than 0.");
                }

                _context.PantryItems.Add(pantryItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAllPantryItems), new { id = pantryItem.Id }, pantryItem);
            }
            catch (StockItemException ex)
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
