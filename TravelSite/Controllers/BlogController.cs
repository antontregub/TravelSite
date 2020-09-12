using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelSite.Data;
using TravelSite.Models;

namespace TravelSite.Controllers
{
    public class BlogController : Controller
    {
        ApplicationDbContext db;

        public BlogController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();

            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;

            var blog = db.Blogs.Where(p=>p.Language == culture.ToString());
            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid? id)
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;

            if (id != null)
            {
                Blog trip = await db.Blogs.Where(p => p.Id == id).Where( p=>p.Language== culture.ToString()).FirstOrDefaultAsync();
                if (trip != null)
                    return View(trip);
            }
            return NotFound();
        }

        public IActionResult Search(string? name)
        {
            IQueryable<Blog> users = db.Blogs.Include(p => p.Title);
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Title.Contains(name));
            }
            return RedirectToAction("Index", users);
        }
    }
}