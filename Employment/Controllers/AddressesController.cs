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
    public class AddressesController : Controller
    {
        private readonly EmploymentContext _context;

        public AddressesController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Addresses.Include(a => a.Company).Include(a => a.Street);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Addresses == null)
                return NotFound();
            

            var address = await _context.Addresses
                .Include(a => a.Company)
                .Include(a => a.Street)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (address == null)
                return NotFound();
            
            return View(address);
        }

        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Id");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,StreetId,HouseNumber,CorpusNumber,FlatNumber,CompanyId")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", address.CompanyId);
            ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Id", address.StreetId);

            return View(address);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Addresses == null)
                return NotFound();
            
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound();
            
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", address.CompanyId);
            ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Id", address.StreetId);

            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StreetId,HouseNumber,CorpusNumber,FlatNumber,CompanyId")] Address address)
        {
            if (id != address.Id)
                return NotFound();
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", address.CompanyId);
            ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Id", address.StreetId);

            return View(address);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Company)
                .Include(a => a.Street)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Addresses == null)
                return Problem("Передаваемый параметр равен null!");
            
            var address = await _context.Addresses.FindAsync(id);

            if (address != null)
                _context.Addresses.Remove(address);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
