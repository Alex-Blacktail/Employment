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
    public class PostsController : Controller
    {
        private readonly EmploymentContext _context;

        public PostsController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Posts.Include(p => p.Company);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
                return NotFound();
            

            var post = await _context.Posts
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortName,BeginDate,EndDate,CompanyId")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", post.CompanyId);
            return View(post);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
                return NotFound();
            
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", post.CompanyId);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortName,BeginDate,EndDate,CompanyId")] Post post)
        {
            if (id != post.Id)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", post.CompanyId);
            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
                return NotFound();

            var post = await _context.Posts
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
                return Problem("Передаваемый параметр равен null!");

            var post = await _context.Posts.FindAsync(id);

            if (post != null)
                _context.Posts.Remove(post);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return _context.Posts.Any(e => e.Id == id);
        }
    }
}
