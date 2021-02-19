
using System;
using insulin_backend.Database.Models;
using Microsoft.EntityFrameworkCore;

// using Microsoft.EntityFrameworkCore;

namespace insulin_backend.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<StepLanguage> StepLanguage { get; set; }
        public DbSet<TutorialLanguage> TutorialLanguages { get; set; }

        public DataContext()
        {
  
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Insulin.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<Tutorial>(p => p.Tutorials).WithMany(t => t.Users);
            
            Language eng = new Language(){Name="English", Id=1};
            
            modelBuilder.Entity<Language>().HasData(eng);
            modelBuilder.Entity<Language>().HasData(new Language(){Name="Danish", Id = 2});
            modelBuilder.Entity<Language>().HasData(new Language(){Name="Polish", Id = 3});
            
            modelBuilder.Entity<User>().HasData(new User(){Id = 1, LanguageId = 1, FirstName = "Dan", LastName = "Mosk", Sex = "Hopefully Today", DateOfBirth = new DateTime(1999, 06, 22), HashedPassword = "bpxgsd2J3", });
        }

    }
}