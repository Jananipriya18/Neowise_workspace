using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/projectrequirements")]
    [ApiController]
    public class ProjectRequirementController : ControllerBase
    {
        private readonly ProjectRequirementService _projectRequirementService;

        public ProjectRequirementController(ProjectRequirementService projectRequirementService)
        {
            _projectRequirementService = projectRequirementService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectRequirement>>> GetAllProjectRequirements()
        {
            var projectRequirements = await _projectRequirementService.GetAllProjectRequirements();
            return Ok(projectRequirements);
        }

        [HttpGet("{requirementId}")]
        public async Task<ActionResult<ProjectRequirement>> GetProjectRequirementById(int requirementId)
        {
            var projectRequirement = await _projectRequirementService.GetProjectRequirementById(requirementId);

            if (projectRequirement == null)
                return NotFound(new { message = "Project requirement not found" });

            return Ok(projectRequirement);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectRequirement>>> GetProjectRequirementsByUserId(int userId)
        {
            var projectRequirements = await _projectRequirementService.GetProjectRequirementsByUserId(userId);

            if (projectRequirements == null || !projectRequirements.Any())
                return NotFound(new { message = "No project requirements found for this user" });

            return Ok(projectRequirements);
        }

        [HttpPost]
        public async Task<ActionResult> AddProjectRequirement([FromBody] ProjectRequirement projectRequirement)
        {
            try
            {
                await _projectRequirementService.AddProjectRequirement(projectRequirement);
                return Ok(new { message = "Project requirement added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{requirementId}")]
        public async Task<ActionResult> UpdateProjectRequirement(int requirementId, [FromBody] ProjectRequirement projectRequirement)
        {
            try
            {
                var success = await _projectRequirementService.UpdateProjectRequirement(requirementId, projectRequirement);

                if (success)
                    return Ok(new { message = "Project requirement updated successfully" });
                else
                    return NotFound(new { message = "Project requirement not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{requirementId}")]
        public async Task<ActionResult> DeleteProjectRequirement(int requirementId)
        {
            try
            {
                var success = await _projectRequirementService.DeleteProjectRequirement(requirementId);

                if (success)
                    return Ok(new { message = "Project requirement deleted successfully" });
                else
                    return NotFound(new { message = "Project requirement not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
