using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcess.Data
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
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                FirtName = "Admin",
                LastName = "Giwu",
                Email = "admin@example.com",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true
            };

            // Hash the password
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");

            // Seed the user
            modelBuilder.Entity<ApplicationUser>().HasData(user);
        }
    }
}
