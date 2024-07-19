using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Exceptions;

namespace dotnetapp.Services
{
    public class CollegeService
    {
        private readonly ApplicationDbContext _context;

        public CollegeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<College>> GetAllColleges()
        {
            return await _context.Colleges.ToListAsync();
        }

        public async Task<College> GetCollegeById(int collegeId)
        {
            return await _context.Colleges.FindAsync(collegeId);
        }

       public async Task AddCollege(College college)
            {
                if (_context.Colleges.Any(c => c.CollegeName == college.CollegeName))
                {
                    throw new CollegeApplicationException("College with the same name already exists");
                }
                _context.Colleges.Add(college);
                await _context.SaveChangesAsync();
            }

        public async Task<bool> UpdateCollege(int collegeId, College college)
        {
            var existingCollege = await _context.Colleges.FindAsync(collegeId);

            if (existingCollege == null)
                return false;

            existingCollege.CollegeName = college.CollegeName;
            existingCollege.Location = college.Location;
            existingCollege.Description = college.Description;
            existingCollege.Website = college.Website;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCollege(int collegeId)
        {
            var college = await _context.Colleges.FindAsync(collegeId);

            if (college == null)
                return false;

            _context.Colleges.Remove(college);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
