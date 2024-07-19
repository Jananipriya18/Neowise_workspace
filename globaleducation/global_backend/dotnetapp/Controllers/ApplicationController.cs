using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _applicationService;

        public ApplicationController(ApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetAllApplications()
        {
            var applications = await _applicationService.GetAllApplications();
            return Ok(applications);
        }

        [HttpGet("{applicationId}")]
        public async Task<ActionResult<Application>> GetApplicationById(int applicationId)
        {
            var application = await _applicationService.GetApplicationById(applicationId);

            if (application == null)
                return NotFound(new { message = "Application not found" });

            return Ok(application);
        }

        [HttpPost]
        public async Task<ActionResult> AddApplication([FromBody] Application application)
        {
            try
            {
                await _applicationService.AddApplication(application);
                return Ok(new { message = "Application added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{applicationId}")]
        public async Task<ActionResult> UpdateApplication(int applicationId, [FromBody] Application application)
        {
            try
            {
                var success = await _applicationService.UpdateApplication(applicationId, application);

                if (success)
                    return Ok(new { message = "Application updated successfully" });
                else
                    return NotFound(new { message = "Application not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{applicationId}")]
        public async Task<ActionResult> DeleteApplication(int applicationId)
        {
            try
            {
                var success = await _applicationService.DeleteApplication(applicationId);

                if (success)
                    return Ok(new { message = "Application deleted successfully" });
                else
                    return NotFound(new { message = "Application not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

         [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplicationsByUserId(int userId)
        {
            var applications = await _applicationService.GetApplicationsByUserId(userId);
            return Ok(applications);
        }

        
    }
}
