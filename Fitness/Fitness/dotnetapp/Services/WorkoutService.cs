using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class WorkoutService
    {
        private readonly ApplicationDbContext _context;

        public WorkoutService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workout>> GetAllWorkouts()
        {
            return await _context.Workouts.ToListAsync();
        }

        public async Task<Workout> GetWorkoutById(int workoutId)
        {
            return await _context.Workouts.FirstOrDefaultAsync(w => w.WorkoutId == workoutId);
        }

        public async Task<bool> AddWorkout(Workout workout)
        {
            if (_context.Workouts.Any(w => w.WorkoutName == workout.WorkoutName))
            {
                throw new WorkoutException("Workout with the same name already exists");
            }
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateWorkout(int workoutId, Workout workout)
        {
            var existingWorkout = await _context.Workouts.FirstOrDefaultAsync(w => w.WorkoutId == workoutId);

            if (existingWorkout == null)
                return false;

            if (_context.Workouts.Any(w => w.WorkoutName == workout.WorkoutName && w.WorkoutId != workoutId))
            {
                throw new WorkoutException("Workout with the same name already exists");
            }

            workout.WorkoutId = workoutId;
            _context.Entry(existingWorkout).CurrentValues.SetValues(workout);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWorkout(int workoutId)
        {
            var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.WorkoutId == workoutId);
            if (workout == null)
                return false;

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
