using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Employment.Data;
using Employment.Models;
using System;
using System.Collections.Generic;

namespace Employment.Controllers
{
    public class PostsController : Controller
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

        public PostsController(EmploymentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employmentContext = _context.Posts
                .Include(p => p.Company);

            return View(await employmentContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
                return NotFound();
            
            var post = await _context.Posts
                .Include(x => x.Company)
                .Include(x => x.Responsibilities)
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
                return NotFound();

            var salary = _context.Salaries.First(x => x.PostId == post.Id);
            var soc = _context.SocialResponsibilities.First(x => x.PostId == post.Id);
            var req = _context.Requirments.Include(x => x.Gender).First(x => x.PostId == post.Id);

            var postViewModel = new PostViewModel
            {
                Post = post,
                Salary = salary,
                SocialResponsibility = soc,
                Requirment = req
            };

            return View(postViewModel);
        }

        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            ViewData["CommunicationSkill"] = new SelectList(_communicationSkills, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreateDto postDto)
        {
            var post = new Post
            {
                Name = postDto.Name,
                ShortName = postDto.ShortName == null ? null : postDto.ShortName,
                BeginDate = postDto.BeginDate,
                CompanyId = postDto.CompanyId
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            var salary = new Salary
            {
                LowerLimit = postDto.LowerLimit,
                UpperLimit = postDto.UpperLimit,
                PostId = post.Id
            };

            var socialRespons = new SocialResponsibility
            {
                EmploymentBook = postDto.EmploymentBook,
                SocialPackage = postDto.SocialPackage,
                PostId = post.Id
            };

            var requirment = new Requirment
            {
                LowerAgeLimit = postDto.LowerAgeLimit,
                UpperAgeLimit = postDto.UpperAgeLimit,
                CommunicationSkill = postDto.CommunicationSkill,
                GenderId = postDto.GenderId,
                PostId = post.Id
            };
            
            _context.Salaries.Add(salary);
            _context.SocialResponsibilities.Add(socialRespons);
            _context.Requirments.Add(requirment);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
                return NotFound();
            
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", post.CompanyId);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id)
                return NotFound();

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
