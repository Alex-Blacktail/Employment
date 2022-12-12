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
    public class ResponsibilitiesController : Controller
    {
        private readonly EmploymentContext _context;

        public ResponsibilitiesController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Responsibilities.Include(r => r.Post);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Responsibilities == null)
                return NotFound();
            

            var responsibility = await _context.Responsibilities
                .Include(r => r.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (responsibility == null)
                return NotFound();
            
            return View(responsibility);
        }

        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,PostId")] Responsibility responsibility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsibility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", responsibility.PostId);

            return View(responsibility);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Responsibilities == null)
                return NotFound();
            
            var responsibility = await _context.Responsibilities.FindAsync(id);

            if (responsibility == null)
                return NotFound();
            
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", responsibility.PostId);

            return View(responsibility);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PostId")] Responsibility responsibility)
        {
            if (id != responsibility.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsibility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsibilityExists(responsibility.Id))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", responsibility.PostId);

            return View(responsibility);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Responsibilities == null)
                return NotFound();

            var responsibility = await _context.Responsibilities
                .Include(r => r.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (responsibility == null)
                return NotFound();

            return View(responsibility);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Responsibilities == null)
            {
                return Problem("Передаваемый параметр равен null!");
            }

            var responsibility = await _context.Responsibilities.FindAsync(id);
            if (responsibility != null)
            {
                _context.Responsibilities.Remove(responsibility);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsibilityExists(int id)
        {
          return _context.Responsibilities.Any(e => e.Id == id);
        }
    }
}
