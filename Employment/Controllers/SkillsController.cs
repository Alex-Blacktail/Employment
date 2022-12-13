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
    public class SkillsController : Controller
    {
        private readonly EmploymentContext _context;

        public SkillsController(EmploymentContext context)
        {
            _context = context;
        }

        public IActionResult Create(int id)
        {
            var skill = new Skill { PostId = id };
            return View(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Skill skill)
        {
            skill.Id = 0;

            _context.Add(skill);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Posts", new { Id = skill.PostId });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Skills == null)
                return NotFound();
            
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Skill skill)
        {
            if (id != skill.Id)
                return NotFound();

            try
            {
                _context.Update(skill);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(skill.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Details", "Posts", new { Id = skill.PostId });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Skills == null)
                return NotFound();

            var skill = await _context.Skills
                .Include(s => s.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Skills == null)
                return Problem("Передаваемый параметр равен null!");
            
            var skill = await _context.Skills.FindAsync(id);

            var postId = skill.PostId;
            _context.Skills.Remove(skill);
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Posts", new { Id = postId });
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
