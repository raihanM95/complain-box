using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComplainBox.Models;
using Microsoft.AspNetCore.Http;

namespace ComplainBox.Controllers
{
    public class NotesController : Controller
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            int id = 0;
            int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out id);
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var notes = _context.notes.Where(c => c.UserId == id).ToList();

                return View(notes);
            }
            else
            {
                return RedirectToAction("Login", "Complains");
            }
        }

        // GET: Notes/Index2
        public async Task<IActionResult> Index2()
        {
            int id = 0;
            int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out id);
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var notes = _context.notes.Where(c => c.UserId == id).ToList();

                return View(notes);
            }
            else
            {
                return RedirectToAction("Login", "Complains");
            }
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                Note noteModel = new Note();

                int paId = 0;
                int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out paId);

                ViewData["UserId"] = noteModel.UserId = paId;

                return View(noteModel);
            }
            else
            {
                return RedirectToAction("Login", "Complains");
            }
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,Name,NoteDescription,UserId")] Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(note);
        }

        // GET: Notes/Create2
        public IActionResult Create2()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                Note noteModel = new Note();

                int paId = 0;
                int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out paId);

                ViewData["UserId"] = noteModel.UserId = paId;

                return View(noteModel);
            }
            else
            {
                return RedirectToAction("Login", "Complains");
            }
        }

        // POST: Notes/Create2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2([Bind("NoteId,Name,NoteDescription,UserId")] Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index2));
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.notes.FindAsync(id);
            _context.notes.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Notes/Delete2/5
        public async Task<IActionResult> Delete2(int id)
        {
            var note = await _context.notes.FindAsync(id);
            _context.notes.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index2));
        }

        private bool NoteExists(int id)
        {
            return _context.notes.Any(e => e.NoteId == id);
        }
    }
}
