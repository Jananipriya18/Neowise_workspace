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
        private readonly AppDbContext _context;

        public FoodController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("restaurant/{restaurantId}/menu")]
        public IActionResult DisplayMenuItemsForRestaurant(int restaurantId)
        {
            var restaurant = _context.Restaurants
                .Where(r => r.Id == restaurantId)
                .Select(r => new 
                { 
                    r.Id, 
                    r.Name, 
                    r.Location, 
                    r.PhoneNumber, 
                    MenuItems = r.MenuItems.Select(mi => new 
                    { 
                        mi.Id, 
                        mi.Name, 
                        mi.Price, 
                        mi.Description 
                    }).ToList() 
                })
                .FirstOrDefault();

            if (restaurant == null)
            {
                return NotFound(); // Handle the case where the restaurant with the given ID doesn't exist
            }

            return Ok(restaurant);
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

            var menuItemWithRestaurant = _context.MenuItems
                .Where(mi => mi.Id == menuItem.Id)
                .Select(mi => new 
                {
                    mi.Id,
                    mi.Name,
                    mi.Price,
                    mi.Description,
                    Restaurant = mi.Restaurant != null ? new 
                    {
                        mi.Restaurant.Id,
                        mi.Restaurant.Name,
                        mi.Restaurant.Location,
                        mi.Restaurant.PhoneNumber
                    } : null
                })
                .FirstOrDefault();

            return CreatedAtAction(nameof(DisplayMenuItemsForRestaurant), new { restaurantId = menuItem.RestaurantId }, menuItemWithRestaurant);
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
