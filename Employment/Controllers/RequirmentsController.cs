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
    public class RequirmentsController : Controller
    {
        private class Communication { public string Id { get; set; } public string Name { get; set; } }

        private List<Communication> _communicationSkills = new List<Communication>()
        {
            new Communication
            {
                Id = "Да",
                Name = "Да"
            },
            new Communication
            {
                Id = "Нет",
                Name = "Нет"
            },
        };

        private readonly EmploymentContext _context;

        public RequirmentsController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Requirments.Include(r => r.Gender).Include(r => r.Post);
            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Requirments == null)
                return NotFound();
            

            var requirment = await _context.Requirments
                .Include(r => r.Gender)
                .Include(r => r.Post)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (requirment == null)
                return NotFound();

            return View(requirment);
        }

        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name");
            ViewData["CommunicationSkill"] = new SelectList(_communicationSkills, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,LowerAgeLimit,UpperAgeLimit,CommunicationSkill,PostId,GenderId")] Requirment requirment)
        {
            _context.Add(requirment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Requirments == null)
                return NotFound();
            
            var requirment = await _context.Requirments.FindAsync(id);

            if (requirment == null)
                return NotFound();

            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", requirment.GenderId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", requirment.PostId);
            ViewData["CommunicationSkill"] = new SelectList(_communicationSkills, "Id", "Name");

            return View(requirment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Requirment requirment)
        {
            if (id != requirment.Id)
                return NotFound();

            try
            {
                _context.Update(requirment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirmentExists(requirment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "Posts", new { Id = requirment.PostId });
        }

        private bool RequirmentExists(int id)
        {
          return _context.Requirments.Any(e => e.Id == id);
        }
    }
}
