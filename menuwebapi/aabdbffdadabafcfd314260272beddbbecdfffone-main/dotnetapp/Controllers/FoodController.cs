using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // DisplayMenuItemsForRestaurant
        [HttpGet("restaurant/{restaurantId}/menu")]
        public IActionResult DisplayMenuItemsForRestaurant(int restaurantId)
        {
            var menuItems = _context.MenuItems
                .Where(mi => mi.RestaurantId == restaurantId)
                .Select(mi => new { mi.Id, mi.Name, mi.Price, mi.Description })
                .ToList();

            return Ok(menuItems);
        }

        // AddMenuItem
        [HttpPost("menu")]
        public IActionResult AddMenuItem([FromBody] MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(DisplayMenuItemsForRestaurant), new { restaurantId = menuItem.RestaurantId }, menuItem);
        }

        // DisplayAllRestaurants
        [HttpGet("restaurants")]
        public IActionResult DisplayAllRestaurants()
        {
            var restaurants = _context.Restaurants
                .Select(r => new { r.Id, r.Name, r.Location, r.PhoneNumber })
                .ToList();

            return Ok(restaurants);
        }

        // SearchRestaurantsByName
        [HttpGet("restaurants/search")]
        public IActionResult SearchRestaurantsByName([FromQuery] string query)
        {
            var restaurants = _context.Restaurants
                .Where(r => r.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(r => new { r.Id, r.Name, r.Location, r.PhoneNumber })
                .ToList();

            return Ok(restaurants);
        }
    }
}
