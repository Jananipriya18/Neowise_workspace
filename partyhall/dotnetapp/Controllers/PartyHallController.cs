using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyHallController : ControllerBase
    {
        private readonly PartyHallService _partyHallService;

        public PartyHallController(PartyHallService partyHallService)
        {
            _partyHallService = partyHallService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyHall>>> Get()
        {
            var partyHalls = await _partyHallService.GetAllPartyHallsAsync();
            return Ok(partyHalls);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PartyHall partyHall)
        {
            try
            {
                if (partyHall == null)
                    return BadRequest("Party hall data is null");

                partyHall.Bookings = null;

                var newPartyHall = await _partyHallService.AddPartyHallAsync(partyHall);
                return CreatedAtAction(nameof(Get), new { id = newPartyHall.PartyHallId }, newPartyHall);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{PartyHallId}")]
        public async Task<IActionResult> Put(long PartyHallId, [FromBody] PartyHall partyHall)
        {
            try
            {
                if (partyHall == null || partyHall.PartyHallId != PartyHallId)
                    return BadRequest("Invalid party hall data");

                var updatedPartyHall = await _partyHallService.UpdatePartyHallAsync(PartyHallId, partyHall);
                if (updatedPartyHall == null)
                {
                    return NotFound();
                }

                return Ok(updatedPartyHall);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{PartyHallId}")]
        public async Task<IActionResult> Delete(long PartyHallId)
        {
            try
            {
                var deletedPartyHall = await _partyHallService.DeletePartyHallAsync(PartyHallId);
                if (deletedPartyHall == null)
                {
                    return NotFound();
                }
                return Ok(deletedPartyHall);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{PartyHallId}")]
        public async Task<ActionResult<PartyHall>> Get(long PartyHallId)
        {
            try
            {
                var partyHall = await _partyHallService.GetPartyHallByIdAsync(PartyHallId);
                if (partyHall == null)
                {
                    return NotFound();
                }
                return Ok(partyHall);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
