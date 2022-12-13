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

            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", salary.PostId);
            return View(salary);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Salaries == null)
                return NotFound();
            
            var salary = await _context.Salaries.FindAsync(id);

            if (salary == null)
                return NotFound();

            var model = new SalaryDto
            {
                Id = salary.Id,
                PostId = salary.PostId,
                LowerLimit = salary.LowerLimit.ToString(),
                UpperLimit = salary.UpperLimit.ToString()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SalaryDto salaryDto)
        {
            if (id != salaryDto.Id)
                return NotFound();

            try
            {
                var salary = new Salary
                {
                    Id = salaryDto.Id,
                    PostId = salaryDto.PostId,
                    LowerLimit = decimal.Parse(salaryDto.LowerLimit),
                    UpperLimit = decimal.Parse(salaryDto.UpperLimit)
                };

                _context.Update(salary);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return RedirectToAction("Details", "Posts", new { Id = salaryDto.PostId });
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }
    }
}
