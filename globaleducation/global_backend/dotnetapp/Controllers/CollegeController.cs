using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace dotnetapp.Controllers
{
    [Route("api/colleges")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly CollegeService _collegeService;

        public CollegeController(CollegeService collegeService)
        {
            _collegeService = collegeService;
        }
        // [Authorize(Roles = "Admin")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<College>>> GetAllColleges()
        {
            var colleges = await _collegeService.GetAllColleges();
            return Ok(colleges);
        }

        [HttpGet("{collegeId}")]
        public async Task<ActionResult<College>> GetCollegeById(int collegeId)
        {
            var college = await _collegeService.GetCollegeById(collegeId);

            if (college == null)
                return NotFound(new { message = "College not found" });

            return Ok(college);
        }

        [HttpPost]
        public async Task<ActionResult> AddCollege([FromBody] College college)
        {
            try
            {
                await _collegeService.AddCollege(college);
                return Ok(new { message = "College added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{collegeId}")]
        public async Task<ActionResult> UpdateCollege(int collegeId, [FromBody] College college)
        {
            try
            {
                var success = await _collegeService.UpdateCollege(collegeId, college);

                if (success)
                    return Ok(new { message = "College updated successfully" });
                else
                    return NotFound(new { message = "College not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{collegeId}")]
        public async Task<ActionResult> DeleteCollege(int collegeId)
        {
            try
            {
                var success = await _collegeService.DeleteCollege(collegeId);

                if (success)
                    return Ok(new { message = "College deleted successfully" });
                else
                    return NotFound(new { message = "College not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
