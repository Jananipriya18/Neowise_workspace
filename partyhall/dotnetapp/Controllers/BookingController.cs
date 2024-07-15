using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;

[Route("/api/")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;
    private readonly UserService _userService; // Inject UserService

    public BookingController(BookingService bookingService, UserService userService)
    {
        _bookingService = bookingService;
        _userService = userService; // Assign injected UserService
    }

    // [Authorize]
    [HttpGet("booking/{bookingId}")]
    public async Task<IActionResult> GetBooking(long bookingId)
    {
        var booking = await _bookingService.GetBookingByIdAsync(bookingId);
        if (booking == null)
        {
            return NotFound();
        }

        return Ok(booking);
    }

    // [Authorize(Roles = "Customer")]
    [HttpGet("user/{UserId}")]
    public async Task<IActionResult> GetBookingsByUserId(long UserId)
    {
        try
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(UserId);
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while fetching bookings for user {UserId}: {ex.Message}");
            return StatusCode(500);
        }
    }

    // [Authorize]
    [HttpGet("booking")]
    public async Task<IActionResult> GetAllBookings()
    {
        try
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while fetching all bookings: {ex.Message}");
            return StatusCode(500);
        }
    }

    // [Authorize(Roles = "Customer")]
    [HttpPost("booking")]
    public async Task<IActionResult> AddBooking([FromBody] Booking booking)
    {
        if (booking == null)
        {
            return BadRequest("Booking data is null");
        }

        try
        {
            if (booking.User != null && booking.User.UserId != booking.UserId)
            {
                booking.User = null;
            }

            var addedBooking = await _bookingService.AddBookingAsync(booking);

            // long? userId = booking.UserId; // Convert nullable long to long, defaulting to 0 if null
            // var user = await _userService.GetUserByIdAsync(userId);
            // if (user == null)
            // {
            //     return BadRequest("User not found");
            // }

            // var response = new
            // {
            //     Booking = addedBooking,
            //     User = user
            // };

            // return Ok(response);

        return Ok(new { Message = "Booking added successfully." });

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding a booking: {ex.Message}");
        }
    }

    // [Authorize(Roles = "Customer")]
    [HttpDelete("booking/{bookingId}")]
    public async Task<IActionResult> DeleteBooking(long bookingId)
    {
        try
        {
            await _bookingService.DeleteBookingAsync(bookingId);
            return Ok(new { Message = "Booking deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting booking: {ex.Message}");
        }
    }

    // [Authorize(Roles = "Admin")]
    [HttpPut("booking/{bookingId}")]
    public async Task<IActionResult> UpdateBooking(long bookingId, [FromBody] Booking updatedBooking)
    {
        if (bookingId != updatedBooking.BookingId)
        {
            return BadRequest();
        }

        var existingBooking = await _bookingService.GetBookingByIdAsync(bookingId);
        if (existingBooking == null)
        {
            return NotFound();
        }

        existingBooking.Status = updatedBooking.Status;

        await _bookingService.UpdateBookingStatusAsync(bookingId, updatedBooking.Status);
        var updatedData = await _bookingService.GetBookingByIdAsync(bookingId);
        return Ok(updatedData);
    }
}
