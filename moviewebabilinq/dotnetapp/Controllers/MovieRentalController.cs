// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using dotnetapp.Models;

// namespace dotnetapp.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class MovieRentalController : ControllerBase
//     {
//         private readonly AppDbContext _context;

//         public MovieRentalController(AppDbContext context)
//         {
//             _context = context;
//         }

//         // Implement a method to display movies associated with a library card.
//         [HttpGet("Customer/{customerId}/Movies")]
//         public IActionResult DisplayMoviesForCustomer(int customerId)
//         {
//             Console.WriteLine(customerId);
//             var movies = _context.Customers.FirstOrDefault(lc => lc.Id == customerId);

//             if (movies == null)
//             {
//                 return NotFound(); // Handle the case where the library card with the given ID doesn't exist
//             }

//             var movies = _context.Movies
//                 .Where(b => b.CustomerId == customerId)
//                 .ToList();

//             return Ok(movies);
//         }

//         // Implement a method to add a movie.
//         [HttpPost("AddMovie")]
//         public IActionResult AddMovie([FromBody] Movie movie)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Movies.Add(movie);
//                 _context.SaveChanges();
//                 return CreatedAtAction(nameof(DisplayMoviesForCustomer), new { customerId = movie.CustomerId }, movie);
//             }
//             return BadRequest(ModelState); // Return the model validation errors
//         }

//         // Implement a method to display all movies in the library.
//         [HttpGet("Movies")]
//         public IActionResult DisplayAllMovies()
//         {
//             var movies = _context.Movies.ToList();
//             return Ok(movies);
//         }

//         // Method to search for movies by title
//         [HttpGet("SearchMoviesByTitle")]
//         public IActionResult SearchMoviesByTitle([FromQuery] string query)
//         {
//             if (string.IsNullOrEmpty(query))
//             {
//                 var allMovies = _context.Movies.ToList();
//                 return Ok(allMovies);
//             }

//             var filteredMovies = _context.Movies
//                 .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
//                 .ToList();

//             return Ok(filteredMovies);
//         }
//     }
// }


using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRentalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieRentalController(AppDbContext context)
        {
            _context = context;
        }

        // Display movies rented by a customer
        // [HttpGet("Customer/{customerId}/Movies")]
        // public async Task<IActionResult> DisplayMoviesForCustomer(int customerId)
        // {
        //     var customer = _context.Customers.FirstOrDefault(c => c.Id == customerId);

        //     if (customer == null)
        //     {
        //         return NotFound(); 
        //     }

        //     var movies = _context.Movies
        //         .Where(m => m.CustomerId == customerId)
        //         .ToList();

        //     return Ok(movies);
        // }


        [HttpGet("Customer/{customerId}/Movies")]
public async Task<IActionResult> DisplayMoviesForCustomer(int customerId)
{
    var customer = await _context.Customers
        .Include(c => c.Movies)
        .FirstOrDefaultAsync(c => c.Id == customerId);

    if (customer == null)
    {
        return NotFound(); 
    }

    var movies = customer.Movies.Select(m => new
    {
        m.Id,
        m.Title,
        m.Director,
        m.ReleaseYear,
        m.CustomerId,
        // Customer details can be included here if needed
        Customer = new
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber
        }
    }).ToList();

    return Ok(movies);
}


        // Add a movie
        // [HttpPost("AddMovie")]
        // public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Movies.Add(movie);
        //         await _context.SaveChangesAsync();
        //         return CreatedAtAction(nameof(DisplayMoviesForCustomer), new { customerId = movie.CustomerId }, movie);
        //     }
        //     return BadRequest(ModelState); // Return the model validation errors
        // }
    

     [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();

                var movieWithCustomer = await _context.Movies
                    .Include(m => m.Customer) // Eagerly load the customer details
                    .Where(m => m.Id == movie.Id)
                    .Select(m => new
                    {
                        m.Id,
                        m.Title,
                        m.Director,
                        m.ReleaseYear,
                        m.CustomerId,
                        Customer = new
                        {
                            Id = m.Customer.Id,
                            Name = m.Customer.Name,
                            Email = m.Customer.Email,
                            PhoneNumber = m.Customer.PhoneNumber
                        }
                    })
                    .FirstOrDefaultAsync();

                if (movieWithCustomer == null)
                {
                    return NotFound(); // Return 404 if movie not found
                }

                return CreatedAtAction(nameof(DisplayMoviesForCustomer), new { customerId = movieWithCustomer.CustomerId }, movieWithCustomer);
            }

            return BadRequest(ModelState); // Return 400 for invalid model state
        }


        

        // Display all movies in the rental store
        // [HttpGet("Movies")]
        // public async Task<IActionResult> DisplayAllMovies()
        // {
        //     var movies = await _context.Movies.ToListAsync();
        //     return Ok(movies);
        // }

    [HttpGet("Movies")]
    public async Task<IActionResult> DisplayAllMovies()
    {
        var movies = await _context.Movies
            .Include(m => m.Customer)
            .Select(m => new
            {
                m.Id,
                m.Title,
                m.Director,
                m.ReleaseYear,
                m.CustomerId,
                Customer = new
                {
                    Id = m.Customer.Id,
                    Name = m.Customer.Name,
                    Email = m.Customer.Email,
                    PhoneNumber = m.Customer.PhoneNumber
                }
            })
            .ToListAsync();

        return Ok(movies);
    }

    // Method to search for movies by title
        [HttpGet("SearchMoviesByTitle")]
        public IActionResult SearchMoviesByTitle([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                var allMovies = _context.Movies.ToList();
                return Ok(allMovies);
            }

            var filteredMovies = _context.Movies
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(filteredMovies);
        }

    }

}
