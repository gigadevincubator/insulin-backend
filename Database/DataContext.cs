
using System;
using System.Collections.ObjectModel;
using insulin_backend.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
            optionsBuilder.EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>()
            //     .HasMany<Tutorial>(p => p.Tutorials).WithMany(t => t.Users);
            
            
        }

    }
}