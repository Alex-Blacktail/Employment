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
    public class ResponsibilitiesController : Controller
    {
        private readonly EmploymentContext _context;

        public ResponsibilitiesController(EmploymentContext context)
        {
            _context = context;
        }

        public IActionResult Create(int id)
        {
            var responsibility = new Responsibility { PostId = id };
            return View(responsibility);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Responsibility responsibility)
        {
            responsibility.Id = 0;

            _context.Add(responsibility);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Posts", new { Id = responsibility.PostId });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Responsibilities == null)
                return NotFound();
            
            var responsibility = await _context.Responsibilities.FindAsync(id);

            if (responsibility == null)
                return NotFound();
            
            return View(responsibility);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Responsibility responsibility)
        {
            if (id != responsibility.Id)
                return NotFound();

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
            return RedirectToAction("Details", "Posts", new { Id = responsibility.PostId });
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
            var postId = responsibility.PostId;

            _context.Responsibilities.Remove(responsibility);     
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Posts", new { Id = postId });
        }

        private bool ResponsibilityExists(int id)
        {
            return _context.Responsibilities.Any(e => e.Id == id);
        }
    }
}
