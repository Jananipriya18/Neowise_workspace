using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/orphanages")]
    [ApiController]
    public class OrphanageController : ControllerBase
    {
        private readonly OrphanageService _orphanageService;

        public OrphanageController(OrphanageService orphanageService)
        {
            _orphanageService = orphanageService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orphanage>>> GetAllOrphanages()
        {
            var orphanages = await _orphanageService.GetAllOrphanages();
            return Ok(orphanages);
        }

        [HttpGet("{orphanageId}")]
        public async Task<ActionResult<Orphanage>> GetOrphanageById(int orphanageId)
        {
            var orphanage = await _orphanageService.GetOrphanageById(orphanageId);

            if (orphanage == null)
                return NotFound(new { message = "Cannot find any orphanage" });

            return Ok(orphanage);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddOrphanage([FromBody] Orphanage orphanage)
        {
            try
            {
                var success = await _orphanageService.AddOrphanage(orphanage);
                if (success)
                    return Ok(new { message = "Orphanage added successfully" });
                else
                    return StatusCode(500, new { message = "Failed to add orphanage" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize]
        [HttpPut("{orphanageId}")]
        public async Task<ActionResult> UpdateOrphanage(int orphanageId, [FromBody] Orphanage orphanage)
        {
            try
            {
                var success = await _orphanageService.UpdateOrphanage(orphanageId, orphanage);

                if (success)
                    return Ok(new { message = "Orphanage updated successfully" });
                else
                    return NotFound(new { message = "Cannot find any orphanage" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize]
        [HttpDelete("{orphanageId}")]
        public async Task<ActionResult> DeleteOrphanage(int orphanageId)
        {
            try
            {
                var success = await _orphanageService.DeleteOrphanage(orphanageId);

                if (success)
                    return Ok(new { message = "Orphanage deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find any orphanage" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
