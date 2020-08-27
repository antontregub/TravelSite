using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelSite.Models;

namespace TravelSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<ImageGallery> ImageGalleries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Blog>().HasKey(u => new { u.Id, u.Language });
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
