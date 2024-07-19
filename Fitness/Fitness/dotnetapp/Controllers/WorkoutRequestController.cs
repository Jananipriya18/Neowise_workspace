using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/workoutrequests")]
    [ApiController]
    public class WorkoutRequestController : ControllerBase
    {
        private readonly WorkoutRequestService _workoutRequestService;

        public WorkoutRequestController(WorkoutRequestService workoutRequestService)
        {
            _workoutRequestService = workoutRequestService;
        }

         [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutRequest>>> GetAllWorkoutRequests()
        {
            var requests = await _workoutRequestService.GetAllWorkoutRequests();
            return Ok(requests);
        }

        [HttpPost]
        public async Task<ActionResult> AddWorkoutRequest([FromBody] WorkoutRequest workoutRequest)
        {
            try
            {
                var success = await _workoutRequestService.AddWorkoutRequest(workoutRequest);
                if (success)
                    return Ok(new { message = "Workout Request added successfully" });
                else
                    return StatusCode(500, new { message = "Failed to add workout request" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

         [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkoutRequest>>> GetWorkoutRequestsByUserId(int userId)
        {
            var requests = await _workoutRequestService.GetWorkoutRequestsByUserId(userId);
            return Ok(requests);
        }

         [Authorize(Roles = "Admin")]
        [HttpPut("{workoutRequestId}")]
        public async Task<ActionResult> UpdateWorkoutRequest(int workoutRequestId, [FromBody] WorkoutRequest workoutRequest)
        {
            try
            {
                var success = await _workoutRequestService.UpdateWorkoutRequest(workoutRequestId, workoutRequest);

                if (success)
                    return Ok(new { message = "Workout Request updated successfully" });
                else
                    return NotFound(new { message = "Cannot find any workout request" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

         [Authorize(Roles = "Admin")]
        [HttpDelete("{workoutRequestId}")]
        public async Task<ActionResult> DeleteWorkoutRequest(int workoutRequestId)
        {
            try
            {
                var success = await _workoutRequestService.DeleteWorkoutRequest(workoutRequestId);

                if (success)
                    return Ok(new { message = "Workout Request deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find any workout request" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
