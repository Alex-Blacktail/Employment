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
    public class PhoneNumbersController : Controller
    {
        private readonly EmploymentContext _context;

        public PhoneNumbersController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.PhoneNumbers.Include(p => p.Company);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhoneNumbers == null)
                return NotFound();

            var phoneNumber = await _context.PhoneNumbers
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (phoneNumber == null)
                return NotFound();

            return View(phoneNumber);
        }

        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,PhoneNumber1,CompanyId")] PhoneNumber phoneNumber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneNumber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", phoneNumber.CompanyId);
            return View(phoneNumber);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhoneNumbers == null)
                return NotFound();
            
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);

            if (phoneNumber == null)
                return NotFound();
            
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", phoneNumber.CompanyId);
            return View(phoneNumber);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhoneNumber1,CompanyId")] PhoneNumber phoneNumber)
        {
            if (id != phoneNumber.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneNumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneNumberExists(phoneNumber.Id))
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

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", phoneNumber.CompanyId);
            return View(phoneNumber);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhoneNumbers == null)
                return NotFound();
            
            var phoneNumber = await _context.PhoneNumbers
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (phoneNumber == null)
                return NotFound();
            
            return View(phoneNumber);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhoneNumbers == null)
                return Problem("Передаваемый параметр равен null!");

            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);

            if (phoneNumber != null)
                _context.PhoneNumbers.Remove(phoneNumber);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneNumberExists(int id)
        {
          return _context.PhoneNumbers.Any(e => e.Id == id);
        }
    }
}
