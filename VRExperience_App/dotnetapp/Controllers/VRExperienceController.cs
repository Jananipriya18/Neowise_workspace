using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        // GET: VRExperience/AvailableVRExperiences
        public async Task<IActionResult> AvailableVRExperiences(string searchString)
        {
            var classes = from c in _context.VRExperiences select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                classes = classes.Where(c => c.VRExperienceName.Contains(searchString));
            }

          

            return View(await classes.ToListAsync());
        }

        // GET: VRExperience/BookedVRExperiences
        public async Task<IActionResult> BookedVRExperiences()
        {
            var classes = await _context.VRExperiences.Include(c => c.Attendees).Where(c => c.Attendees.Any()).ToListAsync();
            return View(classes);
        }

        // GET: VRExperience/DeleteConfirmation/5
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var classEntity = await _context.VRExperiences.FindAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            return View(classEntity);
        }

        // POST: VRExperience/DeleteVRExperience/5
        [HttpPost, ActionName("DeleteVRExperience")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVRExperienceConfirmed(int id)
        {
            var classEntity = await _context.VRExperiences.FindAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            _context.VRExperiences.Remove(classEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AvailableVRExperiences));
        }
    }
}
