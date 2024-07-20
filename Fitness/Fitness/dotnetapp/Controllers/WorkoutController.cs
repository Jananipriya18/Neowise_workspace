using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("api/workout")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutService _workoutService;

        public WorkoutController(WorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

         [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetAllWorkouts()
        {
            var workouts = await _workoutService.GetAllWorkouts();
            return Ok(workouts);
        }

        [HttpGet("{workoutId}")]
        public async Task<ActionResult<Workout>> GetWorkoutById(int workoutId)
        {
            var workout = await _workoutService.GetWorkoutById(workoutId);

            if (workout == null)
                return NotFound(new { message = "Cannot find any workout" });

            return Ok(workout);
        }

         [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddWorkout([FromBody] Workout workout)
        {
            try
            {
                var success = await _workoutService.AddWorkout(workout);
                if (success)
                    return Ok(new { message = "Workout added successfully" });
                else
                    return StatusCode(500, new { message = "Failed to add workout" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

         [Authorize(Roles = "Admin")]
        [HttpPut("{workoutId}")]
        public async Task<ActionResult> UpdateWorkout(int workoutId, [FromBody] Workout workout)
        {
            try
            {
                var success = await _workoutService.UpdateWorkout(workoutId, workout);

                if (success)
                    return Ok(new { message = "Workout updated successfully" });
                else
                    return NotFound(new { message = "Cannot find any workout" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

         [Authorize(Roles = "Admin")]
        [HttpDelete("{workoutId}")]
        public async Task<ActionResult> DeleteWorkout(int workoutId)
        {
            try
            {
                var success = await _workoutService.DeleteWorkout(workoutId);

                if (success)
                    return Ok(new { message = "Workout deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find any workout" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
