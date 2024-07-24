using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    public class VRExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VRExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VRExperience/AvailableExperiences
        public async Task<IActionResult> AvailableExperiences()
        {
            var experiences = from e in _context.VRExperiences
                            orderby e.ExperienceName descending
                            select e;

            return View(await experiences.ToListAsync());
        }


        // GET: VRExperience/BookedExperiences
        public async Task<IActionResult> BookedExperiences()
        {
            var experiences = await _context.VRExperiences
                .Include(e => e.Attendees)
                .Where(e => e.Attendees.Any())
                .ToListAsync();

            return View(experiences);
        }

        // GET: VRExperience/DeleteConfirmation/5
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var experience = await _context.VRExperiences.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            Console.WriteLine("Returned View");
            return View(experience);

        }

        // POST: VRExperience/DeleteExperience/5
        // POST: VRExperience/DeleteExperienceConfirmed/5
        [HttpPost, ActionName("DeleteExperienceConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExperienceConfirmed(int id)
        {
            var experience = await _context.VRExperiences.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            _context.VRExperiences.Remove(experience);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AvailableExperiences));
        }
    }
}
