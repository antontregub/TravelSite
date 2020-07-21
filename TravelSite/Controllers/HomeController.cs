using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelSite.Data;
using TravelSite.Models;

namespace TravelSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Trips.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
         
        [HttpGet]
        public async Task<IActionResult> Show(Guid? id)
        {
            if (id != null)
            {
                Trip trip = await db.Trips.FirstOrDefaultAsync(p => p.Id == id);
                List<Review> reviews =  db.Reviews.Where(n => n.TravelId == trip.Id).ToList();
                trip.Reviews = SortReviews(reviews);

                if (trip != null)
                    return View(trip);
            }
            return NotFound();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // todo tree
        [HttpGet("{id}")]
        public async Task<IActionResult> WriteReview(Guid? id)
        {
            var review = new Review();
            review.TravelId = id.Value;
            review.Id = Guid.NewGuid();
            if (id != null)
            {
                return View(review);
            }
            return NotFound();
        }

        [HttpGet("{id}/{parentid}")]
        public async Task<IActionResult> WriteReview(Guid? id, Guid? parentid)
        {
            var review = new Review();
            review.TravelId = id.Value;
            review.Id = Guid.NewGuid();
            review.ParentId = Guid.Parse(parentid.ToString());
            if (id != null)
            {
                return View(review);
            }
            return NotFound();
        }

        [HttpPost("{id}/{parentid}")]
        public async Task<IActionResult> WriteReview(Review Entity)
        {
            var a = Entity;
            a.Id = Guid.NewGuid();
            db.Reviews.Add(a);
            await db.SaveChangesAsync();
            return View();
        }

        private List<Review> SortReviews(List<Review> list)
        {
            List<Review> sortedList = new List<Review>();

            foreach (var item in list)
            {
                if (item.ParentId == Guid.Empty)
                {
                    sortedList.Add(item);
                }
            }

            sortedList = sortedList.OrderBy(n => n.Data).ToList();

            foreach (var item in list)
            {
                if (item.ParentId != Guid.Empty)
                {
                    var found = sortedList.Find(n => n.Id == item.ParentId);
                    var index = sortedList.IndexOf(found);
                    sortedList.Insert(index + 1, item);
                }
            }

            return sortedList;
        }

    }
}
