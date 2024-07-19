using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class ApplicationService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> GetAllApplications()
        {
            return await _context.Applications.
                       Include(a => a.User).
                       Include(a => a.College).
                         ToListAsync();
        }

       

        public async Task<Application> GetApplicationById(int applicationId)
        {
            return await _context.Applications.FindAsync(applicationId);
        }

        public async Task<bool> AddApplication(Application application)
        {
            bool userApplied = await _context.Applications.AnyAsync(a => a.UserId == application.UserId && a.CollegeId == application.CollegeId);

            if (userApplied)
            {
                throw new CollegeApplicationException("The user has already applied to this college.");
            }
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateApplication(int applicationId, Application application)
        {
            var existingApplication = await _context.Applications.FindAsync(applicationId);

            if (existingApplication == null)
                return false;

            existingApplication.UserId = application.UserId;
            existingApplication.CollegeId = application.CollegeId;
            existingApplication.DegreeName = application.DegreeName;
            existingApplication.TwelfthPercentage = application.TwelfthPercentage;
            existingApplication.PreviousCollege = application.PreviousCollege;
            existingApplication.PreviousCollegeCGPA = application.PreviousCollegeCGPA;
            existingApplication.Status = application.Status;
            existingApplication.CreatedAt = application.CreatedAt;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteApplication(int applicationId)
        {
            var application = await _context.Applications.FindAsync(applicationId);

            if (application == null)
                return false;

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return true;
        }

         public async Task<IEnumerable<Application>> GetApplicationsByUserId(int userId)
        {
            return await _context.Applications
                .Include(a => a.User)
                .Include(a => a.College)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
    }
}
