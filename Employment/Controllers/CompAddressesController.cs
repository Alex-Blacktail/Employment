using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Employment.Data;
using Employment.Models;
using System.Text;

namespace Employment.Controllers
{
    public class CompAddressesController : Controller
    {
        private readonly EmploymentContext _context;

        public CompAddressesController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<CompanyAddressDto>();

            var companies = await _context.Companies
                .Include(e => e.PhoneNumbers)
                .Include(e => e.Addresses)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in companies)
            {
                var fullAddresses = await _context.FullAddresses
                    .Where(x => x.CompanyId == item.Id)
                    .ToListAsync();

                list.Add(new CompanyAddressDto
                {
                    CompanyName = item.Name,
                    FullAddresses = fullAddresses
                });
            }

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var localities = await GetLocalitiesDto();
            var companies = await _context.Companies.ToListAsync();

            ViewData["LocalityId"] = new SelectList(
                localities.Select(x => new
                {
                    Id = x.Id,
                    Name = x.FullName
                }), "Id", "Name");

            ViewData["StreetId"] = new SelectList(_context.Streets.Where(s => s.LocalityId == localities[0].Id), "Id", "Name");

            ViewData["CompanyId"] = new SelectList(
                companies.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name
                }), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            await _context.AddAsync(address);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();           

            var address = await _context.Addresses.Include(a => a.Street).FirstOrDefaultAsync(a => a.Id == id);
            if (address == null)
                return NotFound();

            var localities = await GetLocalitiesDto();
            var companies = await _context.Companies.ToListAsync();

            ViewData["LocalityId"] = new SelectList(
                localities.Select(x => new
                {
                    Id = x.Id,
                    Name = x.FullName
                }), "Id", "Name");


            ViewData["StreetId"] = new SelectList(
                _context.Streets.Where(s => s.LocalityId == address.Street.LocalityId),
                "Id",
                "Name",
                address.StreetId);

            ViewData["CompanyId"] = new SelectList(
                companies.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name
                }), "Id", "Name", address.Id);

            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Address address)
        {
            if (id != address.Id)
                return NotFound();

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var address = await _context.Addresses
                .Include(a => a.Street)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (address == null)
                return NotFound();

            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }

        public IActionResult GetStreetsByLocality(int id)
        {
            ViewData["StreetId"] = new SelectList(_context.Streets.Where(s => s.LocalityId == id), "Id", "Name");
            return PartialView();
        }

        private async Task<List<LocalityDto>> GetLocalitiesDto()
        {
            var localities = await _context.Localities
                .Include(x => x.LocalityType)
                .ToListAsync();

            var localityList = new List<LocalityDto>();

            foreach (var locality in localities)
                localityList.Add(new LocalityDto
                {
                    Id = locality.Id,
                    FullName = $"{locality.LocalityType.ShortName}. {locality.Name}"
                });

            return localityList;
        }
    }
}
