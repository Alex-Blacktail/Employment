using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employment.Data;

namespace Employment.Controllers
{
    public class RequirmentsController : Controller
    {
        private readonly EmploymentContext _context;

        public RequirmentsController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Requirments.Include(r => r.Gender).Include(r => r.Post);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Requirments == null)
                return NotFound();
            

            var requirment = await _context.Requirments
                .Include(r => r.Gender)
                .Include(r => r.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (requirment == null)
                return NotFound();

            return View(requirment);
        }

        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,LowerAgeLimit,UpperAgeLimit,CommunicationSkill,PostId,GenderId")] Requirment requirment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requirment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", requirment.GenderId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", requirment.PostId);
            return View(requirment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Requirments == null)
                return NotFound();
            
            var requirment = await _context.Requirments.FindAsync(id);

            if (requirment == null)
                return NotFound();

            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", requirment.GenderId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", requirment.PostId);

            return View(requirment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LowerAgeLimit,UpperAgeLimit,CommunicationSkill,PostId,GenderId")] Requirment requirment)
        {
            if (id != requirment.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requirment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequirmentExists(requirment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", requirment.GenderId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", requirment.PostId);

            return View(requirment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Requirments == null)
                return NotFound();
            

            var requirment = await _context.Requirments
                .Include(r => r.Gender)
                .Include(r => r.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (requirment == null)
                return NotFound();

            return View(requirment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Requirments == null)
                return Problem("Передаваемый параметр равен null!");  

            var requirment = await _context.Requirments.FindAsync(id);

            if (requirment != null)
                _context.Requirments.Remove(requirment);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirmentExists(int id)
        {
          return _context.Requirments.Any(e => e.Id == id);
        }
    }
}
