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
    public class StreetsController : Controller
    {
        private readonly EmploymentContext _context;

        public StreetsController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Streets.Include(s => s.Locality).Include(s => s.StreetType);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Streets == null)
                return NotFound();
            

            var street = await _context.Streets
                .Include(s => s.Locality)
                .Include(s => s.StreetType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (street == null)
                return NotFound();

            return View(street);
        }

        public IActionResult Create()
        {
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Name");
            ViewData["StreetTypeId"] = new SelectList(_context.StreetTypes, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,StreetTypeId,LocalityId")] Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Add(street);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Name", street.LocalityId);
            ViewData["StreetTypeId"] = new SelectList(_context.StreetTypes, "Id", "Name", street.StreetTypeId);

            return View(street);
        }

        // GET: Streets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Streets == null)
                return NotFound();
            
            var street = await _context.Streets.FindAsync(id);

            if (street == null)
                return NotFound();
            
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Name", street.LocalityId);
            ViewData["StreetTypeId"] = new SelectList(_context.StreetTypes, "Id", "Name", street.StreetTypeId);

            return View(street);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StreetTypeId,LocalityId")] Street street)
        {
            if (id != street.Id)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(street);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreetExists(street.Id))
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

            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Name", street.LocalityId);
            ViewData["StreetTypeId"] = new SelectList(_context.StreetTypes, "Id", "Name", street.StreetTypeId);

            return View(street);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Streets == null)
                return NotFound();

            var street = await _context.Streets
                .Include(s => s.Locality)
                .Include(s => s.StreetType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (street == null)
                return NotFound();
            
            return View(street);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Streets == null)
                return Problem("Передаваемый параметр равен null!");
            
            var street = await _context.Streets.FindAsync(id);

            if (street != null)
                _context.Streets.Remove(street);
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool StreetExists(int id)
        {
            return _context.Streets.Any(e => e.Id == id);
        }
    }
}
