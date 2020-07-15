using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelSite.Models;

namespace TravelSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<ImageGallery> ImageGalleries { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
