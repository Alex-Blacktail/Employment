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
    public class InformationsController : Controller
    {
        //private class Communication { public string Id { get; set; } public string Name { get; set; } }

        //private List<Communication> _communicationSkills = new List<Communication>()
        //{
        //    new Communication
        //    {
        //        Id = "Да",
        //        Name = "Да"
        //    },
        //    new Communication
        //    {
        //        Id = "Нет",
        //        Name = "Нет"
        //    },
        //};

        private readonly EmploymentContext _context;

        public InformationsController(EmploymentContext context)
        {
            _context = context;
        }

        public IActionResult DateWithoutEdu()
        {
            var date = DateTime.Today.AddDays(-7);

            var posts = _context.Posts
                .Include(e => e.Company)
                .Include(e => e.Skills)
                .Where(e => e.BeginDate >= date)
                .OrderBy(e => e.CompanyId)
                .ThenBy(e => e.BeginDate)
                .ToList();

            var list = new List<Post>();

            foreach (var post in posts)
            {
                var buff = post.Skills
                    .Where(e => e.Name.ToLower().Contains("образован"))
                    .ToList();

                if (buff.Count < 1)
                    list.Add(post);
            }

            var model = new DateWithoutEduViewModel
            {
                Date = date,
                Posts = list
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DateWithoutEdu(DateWithoutEduViewModel model)
        {
            var posts = _context.Posts
                .Include(e => e.Company)
                .Include(e => e.Skills)
                .Where(e => e.BeginDate >= model.Date)
                .OrderBy(e => e.CompanyId)
                .ThenBy(e => e.BeginDate)
                .ToList();

            var list = new List<Post>();

            foreach (var post in posts)
            {
                var buff = post.Skills
                    .Where(e => e.Name.ToLower().Contains("образован"))
                    .ToList();

                if (buff.Count < 1)
                    list.Add(post);
            }

            model.Posts = list;

            return View(model);
        }

        public IActionResult DateWithPost()
        {
            var date = DateTime.Today.AddDays(-1);

            var posts = _context.Posts
                .Include(e => e.Company)
                .Where(e => e.BeginDate >= date)
                .OrderBy(e => e.CompanyId)
                .ThenBy(e => e.BeginDate);

            var model = new DateWithPostViewModel();
            model.Date = date;
            model.Posts = posts;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DateWithPost(DateWithPostViewModel model)
        {
            List<Post> posts = null;

            if (string.IsNullOrWhiteSpace(model.Post))
            {
                posts = _context.Posts
                    .Include(e => e.Company)
                    .Include(e => e.Requirments)
                    .Where(e => e.BeginDate >= model.Date)
                    .OrderBy(e => e.CompanyId)
                    .ThenBy(e => e.BeginDate)
                    .ToList();
            }
            else
            {
                var postName = model.Post.ToLower().Trim();

                posts = _context.Posts
                    .Include(e => e.Company)
                    .Include(e => e.Requirments)
                    .Where(e => e.BeginDate >= model.Date)
                    .Where(e => (e.Name.ToLower().Trim()).Contains(postName))
                    .OrderBy(e => e.CompanyId)
                    .ThenBy(e => e.BeginDate)
                    .ToList();
            }
           
            model.Posts = posts == null? new List<Post>() : posts;

            return View(model);
        }

        public IActionResult PeriodMaxPosts()
        {
            var dateBegin = DateTime.Today.AddDays(-7);
            var dateEnd = DateTime.Today;

            var postGroups = _context.Posts
                .Include(e => e.Company)
                .Where(e => dateBegin <= e.BeginDate && e.BeginDate <= dateEnd)
                .GroupBy(e => e.Name)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count(),
                    Posts = g.Select(e => e)
                });

            List<Post> totalPosts = new List<Post>();

            foreach(var postGroup in postGroups)
            {
                var list = new List<Post>();

                if (postGroup.Count > totalPosts.Count)
                    totalPosts = postGroup.Posts.ToList();
            }

            var model = new PeriodMaxPostsViewModel();

            model.BeginDate = dateBegin;
            model.EndDate = dateEnd;

            model.Posts = totalPosts;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PeriodMaxPosts(PeriodMaxPostsViewModel model)
        {
            var dateBegin = DateTime.Today.AddDays(-7);
            var dateEnd = DateTime.Today;

            if (model.BeginDate < model.EndDate)
            {
                dateBegin = model.BeginDate;
                dateEnd = model.EndDate;
            }

            var postGroups = _context.Posts
                .Include(e => e.Company)
                .Where(e => dateBegin <= e.BeginDate && e.BeginDate <= dateEnd)
                .GroupBy(e => e.Name)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count(),
                    Posts = g.Select(e => e)
                });

            List<Post> totalPosts = new List<Post>();

            foreach (var postGroup in postGroups)
            {
                var list = new List<Post>();

                if (postGroup.Count > totalPosts.Count)
                    totalPosts = postGroup.Posts.ToList();
            }

            model.BeginDate = dateBegin;
            model.EndDate = dateEnd;

            model.Posts = totalPosts;

            return View(model);
        }

    }
}
