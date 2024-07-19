using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class WorkoutRequestService
    {
        private readonly ApplicationDbContext _context;

        public WorkoutRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutRequest>> GetAllWorkoutRequests()
        {
            return await _context.WorkoutRequests
                .Include(wr => wr.User)
                .Include(wr => wr.Workout)
                .ToListAsync();
        }

        public async Task<bool> AddWorkoutRequest(WorkoutRequest workoutRequest)
        {
            _context.WorkoutRequests.Add(workoutRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateWorkoutRequest(int workoutRequestId, WorkoutRequest workoutRequest)
        {
            var existingRequest = await _context.WorkoutRequests.FirstOrDefaultAsync(wr => wr.WorkoutRequestId == workoutRequestId);

            if (existingRequest == null)
                return false;

            workoutRequest.WorkoutRequestId = workoutRequestId;
            _context.Entry(existingRequest).CurrentValues.SetValues(workoutRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWorkoutRequest(int workoutRequestId)
        {
            var request = await _context.WorkoutRequests.FirstOrDefaultAsync(wr => wr.WorkoutRequestId == workoutRequestId);
            if (request == null)
                return false;

            _context.WorkoutRequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<WorkoutRequest>> GetWorkoutRequestsByUserId(int userId)
        {
            return await _context.WorkoutRequests
                .Include(wr => wr.User)
                .Include(wr => wr.Workout)
                .Where(wr => wr.UserId == userId)
                .ToListAsync();
        }
    }
}
