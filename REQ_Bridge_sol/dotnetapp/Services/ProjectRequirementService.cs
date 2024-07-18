using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class ProjectRequirementService
    {
        private readonly ApplicationDbContext _context;

        public ProjectRequirementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectRequirement>> GetAllProjectRequirements()
        {
            return await _context.ProjectRequirements.Include(r => r.User).ToListAsync();
        }

        public async Task<ProjectRequirement> GetProjectRequirementById(int requirementId)
        {
            return await _context.ProjectRequirements.FindAsync(requirementId);
        }

        public async Task<IEnumerable<ProjectRequirement>> GetProjectRequirementsByUserId(int userId)
        {
            return await _context.ProjectRequirements
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task AddProjectRequirement(ProjectRequirement projectRequirement)
        {
            _context.ProjectRequirements.Add(projectRequirement);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateProjectRequirement(int requirementId, ProjectRequirement projectRequirement)
        {
            var existingProjectRequirement = await _context.ProjectRequirements.FindAsync(requirementId);

            if (existingProjectRequirement == null)
                return false;

            existingProjectRequirement.UserId = projectRequirement.UserId;
            existingProjectRequirement.RequirementTitle = projectRequirement.RequirementTitle;
            existingProjectRequirement.RequirementDescription = projectRequirement.RequirementDescription;
            existingProjectRequirement.Status = projectRequirement.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectRequirement(int requirementId)
        {
            var projectRequirement = await _context.ProjectRequirements.FindAsync(requirementId);

            if (projectRequirement == null)
                return false;

            _context.ProjectRequirements.Remove(projectRequirement);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
