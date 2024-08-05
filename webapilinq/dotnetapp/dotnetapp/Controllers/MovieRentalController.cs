using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class MovieRentalController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MovieRentalController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("DisplayMoviesForCustomer/{customerId}")]
    public async Task<ActionResult<IEnumerable<Movie>>> DisplayMoviesForCustomer(int customerId)
    {
        var movies = await _context.Movies
            .Where(m => m.CustomerId == customerId)
            .ToListAsync();

        if (!movies.Any())
        {
            return NotFound();
        }

        return Ok(movies);
    }

    // [HttpPost("AddMovie")]
    // public async Task<ActionResult<Movie>> AddMovie([FromBody] Movie movie)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }

    //     _context.Movies.Add(movie);
    //     await _context.SaveChangesAsync();

    //     return CreatedAtAction(nameof(AddMovie), new { id = movie.Id }, movie);
    // }

    [HttpPost("AddMovie")]
public async Task<ActionResult<Movie>> AddMovie([FromBody] Movie movie)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Fetch the customer based on the customerId
    var customer = await _context.Customers.FindAsync(movie.CustomerId);
    if (customer == null)
    {
        return NotFound(new { Message = "Customer not found" });
    }

    // Associate the fetched customer with the movie
    movie.Customer = customer;

    _context.Movies.Add(movie);
    await _context.SaveChangesAsync();

    // Include the customer when returning the movie
    var addedMovie = await _context.Movies
        .Include(m => m.Customer) // Include the Customer navigation property
        .SingleOrDefaultAsync(m => m.Id == movie.Id);

    return CreatedAtAction(nameof(AddMovie), new { id = addedMovie.Id }, addedMovie);
}


    [HttpGet("DisplayAllMovies")]
    public async Task<ActionResult<IEnumerable<Movie>>> DisplayAllMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return Ok(movies);
    }

    [HttpGet("SearchMoviesByTitle/{query}")]
    public async Task<ActionResult<IEnumerable<Movie>>> SearchMoviesByTitle(string query)
    {
        var movies = await _context.Movies
            .Where(m => m.Title.Contains(query))
            .ToListAsync();

        if (!movies.Any())
        {
            return NotFound();
        }

        return Ok(movies);
    }
 }
}
