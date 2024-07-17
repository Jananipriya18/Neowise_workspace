using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/donations")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly DonationService _donationService;

        public DonationController(DonationService donationService)
        {
            _donationService = donationService;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donation>>> GetAllDonations()
        {
            var donations = await _donationService.GetAllDonations();
            return Ok(donations);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddDonation([FromBody] Donation donation)
        {
            try
            {
                var success = await _donationService.AddDonation(donation);
                if (success)
                    return Ok(new { message = "Donation added successfully" });
                else
                    return StatusCode(500, new { message = "Failed to add donation" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Donation>>> GetDonationsByUserId(int userId)
        {
            var donations = await _donationService.GetDonationsByUserId(userId);
            return Ok(donations);
        }
    
        [HttpGet("orphanage/{orphanageId}")]
        public async Task<ActionResult<IEnumerable<Donation>>> GetDonationsByOrphanageId(int OrphanageId)
        {
            var donations = await _donationService.GetDonationsByOrphanageId(OrphanageId);
            return Ok(donations);
        }
    }
}
