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
    public class LocalitiesController : Controller
    {
        private readonly EmploymentContext _context;

        public LocalitiesController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Localities.Include(l => l.LocalityType);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Localities == null)
                return NotFound();

            var locality = await _context.Localities
                .Include(l => l.LocalityType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (locality == null)
                return NotFound();

            return View(locality);
        }

        public IActionResult Create()
        {
            ViewData["LocalityTypeId"] = new SelectList(_context.LocalityTypes, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,LocalityTypeId")] Locality locality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LocalityTypeId"] = new SelectList(_context.LocalityTypes, "Id", "Id", locality.LocalityTypeId);
            return View(locality);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Localities == null)
                return NotFound();

            var locality = await _context.Localities.FindAsync(id);
            if (locality == null)
                return NotFound();
            

            ViewData["LocalityTypeId"] = new SelectList(_context.LocalityTypes, "Id", "Id", locality.LocalityTypeId);
            return View(locality);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LocalityTypeId")] Locality locality)
        {
            if (id != locality.Id)
                return NotFound();
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalityExists(locality.Id))
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

            ViewData["LocalityTypeId"] = new SelectList(_context.LocalityTypes, "Id", "Id", locality.LocalityTypeId);
            return View(locality);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Localities == null)
                return NotFound();

            var locality = await _context.Localities
                .Include(l => l.LocalityType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (locality == null)
                return NotFound();
            
            return View(locality);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Localities == null)
                return Problem("Передаваемый параметр равен null!");
            
            var locality = await _context.Localities.FindAsync(id);

            if (locality != null)
                _context.Localities.Remove(locality);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalityExists(int id)
        {
          return _context.Localities.Any(e => e.Id == id);
        }
    }
}
