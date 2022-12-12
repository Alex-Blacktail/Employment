using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employment.Data;
using Employment.Models;
using System.Text;

namespace Employment.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly EmploymentContext _context;

        public CompaniesController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<CompanyDto>();

            var companies = await _context.Companies
                .Include(e => e.PhoneNumbers)
                .AsNoTracking()
                .ToListAsync();

            foreach(var item in companies)
            {
                list.Add(new CompanyDto
                {
                    Company = item
                });
            }

            return View(list);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var list = new List<CompanyDto>();

        //    var companies = await _context.Companies
        //        .Include(e => e.PhoneNumbers)
        //        .Include(e => e.Addresses)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    foreach (var item in companies)
        //    {
        //        string addresses = null;
        //        var fullAddresses = _context.FullAddresses.Where(x => x.CompanyId == item.Id);

        //        if (fullAddresses.Count() > 0)
        //        {
        //            var sb = new StringBuilder(16);
        //            foreach (var addr in fullAddresses)
        //                sb.Append(addr.ToString()).Append("\n");

        //            addresses = sb.ToString();
        //        }

        //        list.Add(new CompanyDto
        //        {
        //            Company = item,
        //            FullAddress = addresses != null ? addresses : null
        //        });
        //    }

        //    return View(list);
        //}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companies == null)
                return NotFound();
            
            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (company == null)
                return NotFound();
            
            return View(company);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortName,Email")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companies == null)
                return NotFound();

            var company = await _context.Companies.FindAsync(id);

            if (company == null)
                return NotFound();

            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortName,Email")] Company company)
        {
            if (id != company.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (company == null)
                return NotFound();

            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Companies == null)
                return Problem("Передаваемый параметр равен null!");
            
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
                _context.Companies.Remove(company);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
