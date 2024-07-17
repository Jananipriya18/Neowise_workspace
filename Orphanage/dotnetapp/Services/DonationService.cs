using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class DonationService
    {
        private readonly ApplicationDbContext _context;

        public DonationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Donation>> GetAllDonations()
        {
            return await _context.Donations.Include(r => r.User).Include(r => r.Orphanage).ToListAsync();
        }
       public async Task<IEnumerable<Donation>> GetDonationsByUserId(int userId)
        {
            return await _context.Donations
                .Where(c => c.UserId == userId)
                .Include(r => r.Orphanage)
                .ToListAsync();
        }
        public async Task<Donation> GetDonationById(int donationId)
        {
            return await _context.Donations.FirstOrDefaultAsync(d => d.DonationId == donationId);
        }

        public async Task<bool> AddDonation(Donation donation)
        {
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();
            return true;
        }
            public async Task<IEnumerable<Donation>> GetDonationsByOrphanageId(int orphanageId)
        {
            return await _context.Donations
                .Where(d => d.OrphanageId == orphanageId)
                .ToListAsync();
        }

        public async Task<bool> UpdateDonation(int donationId, Donation donation)
        {
            var existingDonation = await _context.Donations.FirstOrDefaultAsync(d => d.DonationId == donationId);

            if (existingDonation == null)
                return false;

            _context.Entry(existingDonation).CurrentValues.SetValues(donation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDonation(int donationId)
        {
            var donation = await _context.Donations.FirstOrDefaultAsync(d => d.DonationId == donationId);
            if (donation == null)
                return false;

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
