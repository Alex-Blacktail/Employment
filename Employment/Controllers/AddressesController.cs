using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Employment.Data;
using System.Collections.Generic;
using Employment.Models;

namespace Employment.Controllers
{
    public class AddressesController : Controller
    {
        private readonly EmploymentContext _context;

        public AddressesController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var addresses = await _context.FullAddresses.ToListAsync();
            return View(addresses);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Street)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        public async Task<IActionResult> Create()
        {
            var localities = await GetLocalitiesDto();

            ViewData["LocalityId"] = new SelectList(
                localities.Select(x => new
                {
                    Id = x.Id,
                    Name = x.FullName
                }), "Id", "Name");

            ViewData["StreetId"] = new SelectList(_context.Streets.Where(s => s.LocalityId == localities[0].Id), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,StreetId,HouseNumber,EntranceNumber,FloorNumber")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var localities = await GetLocalitiesDto();

            ViewData["LocalityId"] = new SelectList(
                localities.Select(x => new
                {
                    Id = x.Id,
                    Name = x.FullName
                }), "Id", "Name");

            ViewData["StreetId"] = new SelectList(_context.Streets.Where(s => s.LocalityId == localities[0].Id), "Id", "Name");

            return View(address);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.Include(a => a.Street).FirstOrDefaultAsync(a => a.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            var localities = await GetLocalitiesDto();

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
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StreetId,HouseNumber,EntranceNumber,FloorNumber")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            ViewData["StreetId"] = new SelectList(_context.Streets, "Id", "Name", address.StreetId);
            return View(address);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Street)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

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
