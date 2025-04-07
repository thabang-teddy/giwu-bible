using Microsoft.EntityFrameworkCore;
using Models.SyncModels.BibleSQlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    // DbContexts/SqliteDbContext.cs
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

        public DbSet<BibleVersionKey> BibleVersionKeys { get; set; }
        public DbSet<KeyAbbreviationsEnglish> KeyAbbreviationsEnglish { get; set; }
        public DbSet<KeyEnglish> KeyEnglish { get; set; }
        public DbSet<KeyGenreEnglish> KeyGenreEnglish { get; set; }

        // For multiple verse tables like t_asv, t_bbe you can add dynamically or hardcode:
        public DbSet<Verses> T_Asv { get; set; }
        public DbSet<Verses> T_Bbe { get; set; }
        public DbSet<Verses> T_Kjv { get; set; }
        public DbSet<Verses> T_Web { get; set; }
        public DbSet<Verses> T_Ylt { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BibleVersionKey>().ToTable("bible_version_key");
            modelBuilder.Entity<KeyAbbreviationsEnglish>().ToTable("key_abbreviations_english");
            modelBuilder.Entity<KeyEnglish>().ToTable("key_english");
            modelBuilder.Entity<KeyGenreEnglish>().ToTable("key_genre_english");

            modelBuilder.Entity<Verses>().ToTable("t_asv");
            modelBuilder.Entity<Verses>().ToTable("t_bbe");
            modelBuilder.Entity<Verses>().ToTable("t_kjv");
            modelBuilder.Entity<Verses>().ToTable("t_web");
            modelBuilder.Entity<Verses>().ToTable("t_ylt");
        }
    }
}
