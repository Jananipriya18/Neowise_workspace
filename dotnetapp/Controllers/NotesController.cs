using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NotesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Notes/getAllNote
    [HttpGet("getAllNote")]
    public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
    {
        return await _context.Notes.ToListAsync();
    }

    // POST: api/Notes/addNote
    [HttpPost("addNote")]
    public async Task<ActionResult<Note>> AddNote(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        // Return the created note with a 201 status code
        return CreatedAtAction(nameof(GetAllNotes), new { id = note.Id }, note);
    }
}
