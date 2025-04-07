using DataAccess.Data.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.SyncModels.BibleSQlite;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BookMark> BookMarks { get; set; }
        public DbSet<Bible> Bibles { get; set; }
        public DbSet<BibleBook> BibleBooks { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bible>()
                .HasOne(b => b.BibleBook)
                .WithOne(bb => bb.Bible)
                .HasForeignKey<BibleBook>(bb => bb.BibleId);

            // Apply seed data from different files
            UsersSeed.Apply(modelBuilder);

        }
    }

}
