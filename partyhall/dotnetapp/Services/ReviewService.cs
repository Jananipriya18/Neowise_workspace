using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                                 .Include(r => r.User) // Include user data
                                 .ToListAsync();
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(long userId)
        {
            return await _context.Reviews
                                 .Where(r => r.UserId == userId)
                                 .Include(r => r.User) // Include user data
                                 .ToListAsync();
        }
    }
}
