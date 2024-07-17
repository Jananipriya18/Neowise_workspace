using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Exceptions;

namespace dotnetapp.Services
{
    public class OrphanageService
    {
        private readonly ApplicationDbContext _context;

        public OrphanageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orphanage>> GetAllOrphanages()
        {
            return await _context.Orphanages.ToListAsync();
        }

        public async Task<Orphanage> GetOrphanageById(int orphanageId)
        {
            return await _context.Orphanages.FirstOrDefaultAsync(o => o.OrphanageId == orphanageId);
        }

        public async Task<bool> AddOrphanage(Orphanage orphanage)
        {

             var existingOrphanage = await _context.Orphanages.FirstOrDefaultAsync(c => c.OrphanageName == orphanage.OrphanageName);
           if (existingOrphanage != null)
            {
                 throw new OrphanageException("Orphanage with the same name already exists");
            }
            _context.Orphanages.Add(orphanage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrphanage(int orphanageId, Orphanage orphanage)
        {
            var existingOrphanage = await _context.Orphanages.FirstOrDefaultAsync(o => o.OrphanageId == orphanageId);

            if (existingOrphanage == null)
                return false;

            orphanage.OrphanageId = orphanageId;
            _context.Entry(existingOrphanage).CurrentValues.SetValues(orphanage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrphanage(int orphanageId)
        {
            var orphanage = await _context.Orphanages.FirstOrDefaultAsync(o => o.OrphanageId == orphanageId);
            if (orphanage == null)
                return false;

            _context.Orphanages.Remove(orphanage);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
