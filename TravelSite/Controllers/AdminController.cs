using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelSite.Data;
using TravelSite.Models;

namespace TravelSite.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class AdminController : Controller
    {
        ApplicationDbContext db;

        public AdminController(ApplicationDbContext context)
        {
            db = context;
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id != null)
            {
                Trip trip = await db.Trips.FirstOrDefaultAsync(p => p.Id == id);
                if (trip != null)
                    return View(trip);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Trip trip)
        {
            db.Trips.Update(trip);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Show()
        {
            return View(db.Trips.ToList());
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create(Trip Entity)
        {
            var a = Entity;
            db.Trips.Add(a);
            await db.SaveChangesAsync();
            return View();
        }
    }
}