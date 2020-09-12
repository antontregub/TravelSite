using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
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
        // todo validation and fixed name in text
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

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> EditPost(Guid? id)
        {
            if (id != null)
            {
                Blog blog = await db.Blogs.Where(p => p.Id == id).FirstOrDefaultAsync();

                BlogCreate blogCreate = new BlogCreate
                {
                    Id = Guid.NewGuid(),
                    Language = new CultureInfo( blog.Language.ToString()),
                    Title = blog.Title,
                    Tag = blog.Tag,
                    ShortContent = blog.ShortContent,
                    Date = DateTime.Now,
                    Content = blog.Content
                };

                if (blogCreate != null)
                    return View(blogCreate);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(BlogCreate entity)
        {
           Blog blog = new Blog
            {
                Id = entity.Id,
                Language = entity.Language.ToString(),
                Title = entity.Title,
                Tag = entity.Tag,
                ShortContent = entity.ShortContent,
                Date = DateTime.Now,
                Content = entity.Content
            };
            if (entity.MainPhoto != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(entity.MainPhoto.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)entity.MainPhoto.Length);
                }

                // установка массива байтов
                blog.MainPhoto = imageData;
            }

            db.Blogs.Update(blog);
            db.Entry(blog).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Show()
        {
            return View(db.Trips.ToList());
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> TripDetail(Guid? id)
        {
            if (id != null)
            {
                Trip trip = await db.Trips.FirstOrDefaultAsync(p => p.Id == id);
                List<Review> reviews = db.Reviews.Where(n => n.TravelId == trip.Id).ToList();
                trip.Reviews = SortReviews(reviews);

                if (trip != null)
                    return View(trip);
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create()
        {
            return View();
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

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                Trip trip = await db.Trips.FirstOrDefaultAsync(p => p.Id == id);

                if (trip != null)
                {
                    db.Trips.Remove(trip);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult ShowImage()
        {
           var a =  db.ImageGalleries.ToList();
            return View(a);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult ShowPost()
        {
            var a = db.Blogs.ToList();
            return View(a);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> CreateImage(ImageCreate entity)
        {
            ImageGallery person = new ImageGallery
            {
                Id = Guid.NewGuid(),
                Country= entity.Country,
                City =  entity.City,
                Description = entity.Description,
                Data = entity.Data

            };
            if (entity.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(entity.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)entity.Image.Length);
                }
                // установка массива байтов
                person.Image = imageData;
            }
            db.ImageGalleries.Add(person);
            db.SaveChanges();

            return View();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> CreateImage()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> EditImage(Guid? id)
        {
            if (id != null)
            {
                ImageGallery image = await db.ImageGalleries.FirstOrDefaultAsync(p => p.Id == id);
                if (image != null)
                    return View(image);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditImage(ImageGallery image)
        {
            db.ImageGalleries.Update(image);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteImage(Guid? id)
        {
            if (id != null)
            {
                ImageGallery trip = await db.ImageGalleries.FirstOrDefaultAsync(p => p.Id == id);

                if (trip != null)
                {
                    var a = db.ImageGalleries.ToList();
                    //a.Remove(trip);
                    db.ImageGalleries.Remove(trip);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            if (id != null)
            {
                Blog trip = await db.Blogs.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (trip != null)
                {
                    var a = db.ImageGalleries.ToList();
                    //a.Remove(trip);
                    db.Blogs.Remove(trip);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteReview(Guid? id)
        {
            if (id != null)
            {
                Review trip = await db.Reviews.FirstOrDefaultAsync(p => p.Id == id);

                if (trip != null)
                {
                    db.Reviews.Remove(trip);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();
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


        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> CreatePost()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> CreatePost(BlogCreate entity)
        {
            var blogs =  db.Trips.Where(p => p.Id == entity.Id);

            Blog blog = new Blog
                {
                    Id = Guid.NewGuid(),
                    Language = entity.Language.ToString(),
                    Title = entity.Title,
                    Tag = entity.Tag,
                    ShortContent = entity.ShortContent,
                    Date = DateTime.Now,
                    Content = entity.Content
                };
                if (entity.MainPhoto != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(entity.MainPhoto.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int) entity.MainPhoto.Length);
                    }

                    // установка массива байтов
                    blog.MainPhoto = imageData;
                }

                db.Blogs.Add(blog);
            await db.SaveChangesAsync();
            return View();
        }
    }
}