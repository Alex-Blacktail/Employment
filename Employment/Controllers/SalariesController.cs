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
    public class SalariesController : Controller
    {
        private readonly EmploymentContext _context;

        public SalariesController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Salaries.Include(s => s.Post);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Salaries == null)
                return NotFound();
            
            var salary = await _context.Salaries
                .Include(s => s.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salary == null)
                return NotFound();

            return View(salary);
        }

        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,LowerLimit,UpperLimit,PostId")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", salary.PostId);
            return View(salary);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Salaries == null)
                return NotFound();
            
            var salary = await _context.Salaries.FindAsync(id);

            if (salary == null)
                return NotFound();

            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", salary.PostId);

            return View(salary);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LowerLimit,UpperLimit,PostId")] Salary salary)
        {
            if (id != salary.Id)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.Id))
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

            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", salary.PostId);
            return View(salary);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Salaries == null)
                return NotFound();
            
            var salary = await _context.Salaries
                .Include(s => s.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salary == null)
                return NotFound();           

            return View(salary);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Salaries == null)
                return Problem("Передаваемый параметр равен null!");
            
            var salary = await _context.Salaries.FindAsync(id);

            if (salary != null)
                _context.Salaries.Remove(salary);      
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }
    }
}
