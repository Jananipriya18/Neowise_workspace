using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Class/AvailableClasses
        public async Task<IActionResult> AvailableClasses(string searchString)
        {
            var classes = from c in _context.Classes select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                classes = classes.Where(c => c.ClassName.Contains(searchString));
            }

          

            return View(await classes.ToListAsync());
        }

        // GET: Class/BookedClasses
        public async Task<IActionResult> BookedClasses()
        {
            var classes = await _context.Classes.Include(c => c.Students).Where(c => c.Students.Any()).ToListAsync();
            return View(classes);
        }

        // GET: Class/DeleteConfirmation/5
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            return View(classEntity);
        }

        // POST: Class/DeleteClass/5
        [HttpPost, ActionName("DeleteClass")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClassConfirmed(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AvailableClasses));
        }
    }
}
