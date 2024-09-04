using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicRecordController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MusicRecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // // GET: api/MusicRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetMusicRecords()
        {
            // Retrieve all music records including their associated orders
            var musicRecords = await _context.MusicRecords
                .Include(mr => mr.Order) // Include related Order
                .ToListAsync();

            if (musicRecords == null || musicRecords.Count == 0)
            {
                return NoContent(); // Return 204 No Content if no records are found
            }

            // Project the results to avoid cyclic references
            var musicRecordResults = musicRecords.Select(mr => new
            {
                mr.MusicRecordId,
                mr.Artist,
                mr.Album,
                mr.Genre,
                mr.Price,
                mr.StockQuantity,
                Order = mr.Order != null ? new
                {
                    mr.Order.OrderId,
                    mr.Order.CustomerName,
                    mr.Order.OrderDate
                } : null
            }).ToList();

            return Ok(musicRecordResults); // Return 200 OK with the list of MusicRecords and their Orders
        }


    
        // GET: api/MusicRecord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetMusicRecord(int id)
        {
            // Retrieve a single music record by ID including its associated order
            var musicRecord = await _context.MusicRecords
                .Include(mr => mr.Order) // Include related Order
                .FirstOrDefaultAsync(mr => mr.MusicRecordId == id);

            if (musicRecord == null)
            {
                return NotFound();
            }

            // Project the result to include Order details
            var musicRecordResult = new
            {
                musicRecord.MusicRecordId,
                musicRecord.Artist,
                musicRecord.Album,
                musicRecord.Genre,
                musicRecord.Price,
                musicRecord.StockQuantity,
                Order = musicRecord.Order != null ? new
                {
                    musicRecord.Order.OrderId,
                    musicRecord.Order.CustomerName,
                    musicRecord.Order.OrderDate
                } : null
            };

            return Ok(musicRecordResult); // Return 200 OK with the MusicRecord and its Order
        }
    

        [HttpPost]
    public async Task<ActionResult<MusicRecord>> CreateMusicRecord(MusicRecord musicRecord)
    {
        // Ensure that OrderId is provided if it's not nullable
        if (musicRecord.OrderId == null)
        {
            return BadRequest(new { message = "OrderId is required" });
        }

        // Add the music record to the database
        _context.MusicRecords.Add(musicRecord);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMusicRecord), new { id = musicRecord.MusicRecordId }, musicRecord);
    }


        // PUT: api/MusicRecord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMusicRecord(int id, MusicRecord musicRecord)
        {
            if (id != musicRecord.MusicRecordId)
            {
                return BadRequest();
            }

            _context.Entry(musicRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/MusicRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusicRecord(int id)
        {
            var musicRecord = await _context.MusicRecords.FindAsync(id);
            if (musicRecord == null)
            {
                return NotFound();
            }

            _context.MusicRecords.Remove(musicRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MusicRecordExists(int id)
        {
            return _context.MusicRecords.Any(mr => mr.MusicRecordId == id);
        }
    }
}
