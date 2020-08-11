using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var blog = db.Blogs.ToList();
            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid? id)
        {
            if (id != null)
            {
                Blog trip = await db.Blogs.FirstOrDefaultAsync(p => p.Id == id);
                if (trip != null)
                    return View(trip);
            }
            return NotFound();
        }
    }
}