using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Exceptions;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class PartyHallService
    {
        private readonly ApplicationDbContext _context;

        public PartyHallService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PartyHall>> GetAllPartyHallsAsync()
        {
            return await _context.PartyHalls.ToListAsync();
        }

        public async Task<PartyHall> AddPartyHallAsync(PartyHall partyHall)
        {
            if (_context.PartyHalls.Any(c => c.HallName == partyHall.HallName))
            {
                throw new PartyHallException("A party hall with the same name already exists");
            }
            _context.PartyHalls.Add(partyHall);
            await _context.SaveChangesAsync();
            return partyHall;
        }

        public async Task<PartyHall> UpdatePartyHallAsync(long id, PartyHall partyHall)
        {
            var existingPartyHall = await _context.PartyHalls.FindAsync(id);
            if (existingPartyHall == null)
            {
                return null; // Party hall not found
            }

            // Update existing party hall properties with the new values
            existingPartyHall.HallName = partyHall.HallName;
            existingPartyHall.HallImageUrl = partyHall.HallImageUrl;
            existingPartyHall.HallLocation = partyHall.HallLocation;
            existingPartyHall.HallAvailableStatus = partyHall.HallAvailableStatus;
            existingPartyHall.Price = partyHall.Price;
            existingPartyHall.Capacity = partyHall.Capacity;
            existingPartyHall.Description = partyHall.Description;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return existingPartyHall;
        }

        public async Task<PartyHall> DeletePartyHallAsync(long id)
        {
            var partyHall = await _context.PartyHalls.FindAsync(id);
            if (partyHall == null)
            {
                return null;
            }

            _context.PartyHalls.Remove(partyHall);
            await _context.SaveChangesAsync();
            return partyHall;
        }

        public async Task<PartyHall> GetPartyHallByIdAsync(long id)
        {
            return await _context.PartyHalls.FindAsync(id);
        }
    }
}
