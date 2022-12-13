using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employment.Data;
using Employment.Models;

namespace Employment.Controllers
{
    public class SocialResponsibilitiesController : Controller
    {
        private readonly EmploymentContext _context;

        public SocialResponsibilitiesController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.SocialResponsibilities.Include(s => s.Post);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SocialResponsibilities == null)
                return NotFound();
            
            var socialResponsibility = await _context.SocialResponsibilities
                .Include(s => s.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (socialResponsibility == null)
                return NotFound();

            return View(socialResponsibility);
        }

        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmploymentBook,SocialPackage,PostId")] SocialResponsibility socialResponsibility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socialResponsibility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", socialResponsibility.PostId);

            return View(socialResponsibility);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SocialResponsibilities == null)
                return NotFound();

            var socialResponsibility = await _context.SocialResponsibilities.FindAsync(id);

            if (socialResponsibility == null)
                return NotFound();

            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", socialResponsibility.PostId);

            return View(socialResponsibility);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SocialResponsibility socialResponsibility)
        {
            if (id != socialResponsibility.Id)
                return NotFound();

            try
            {
                _context.Update(socialResponsibility);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialResponsibilityExists(socialResponsibility.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "Posts", new { Id = socialResponsibility.PostId });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SocialResponsibilities == null)
                return NotFound();
            
            var socialResponsibility = await _context.SocialResponsibilities
                .Include(s => s.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (socialResponsibility == null)
                return NotFound();
            
            return View(socialResponsibility);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SocialResponsibilities == null)
                return Problem("Передаваемый параметр равен null!");
            
            var socialResponsibility = await _context.SocialResponsibilities.FindAsync(id);

            if (socialResponsibility != null)
                _context.SocialResponsibilities.Remove(socialResponsibility);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialResponsibilityExists(int id)
        {
            return _context.SocialResponsibilities.Any(e => e.Id == id);
        }
    }
}
