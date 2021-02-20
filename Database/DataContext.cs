
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
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>()
            //     .HasMany<Tutorial>(p => p.Tutorials).WithMany(t => t.Users);
            
            Language eng = new Language(){Name="English", Id=1};
            
            modelBuilder.Entity<Language>().HasData(eng);
            modelBuilder.Entity<Language>().HasData(new Language(){Name="Danish", Id = 2});
            modelBuilder.Entity<Language>().HasData(new Language(){Name="Polish", Id = 3});
            
            modelBuilder.Entity<User>().HasData(new User(){Id = 1, LanguageId = 1, FirstName = "Dan", LastName = "Mosk", Sex = "Hopefully Today", DateOfBirth = new DateTime(1999, 06, 22), HashedPassword = "bpxgsd2J3", });

            modelBuilder.Entity<Tutorial>().HasData(new Tutorial(){Color = "#FF0000", Id = 1});
            
            modelBuilder.Entity<TutorialLanguage>().HasData(new TutorialLanguage()
                {Id = 1, LanguageId = 1, Title = "How to survive",TutorialId = 1, UserId = 1});
            
            modelBuilder.Entity<Step>().HasData(new Step() {Id = 1, StepNumber = 1, TutorialId = 1, Video = "vid"});
            modelBuilder.Entity<Step>().HasData(new Step() {Id = 2, StepNumber = 2, TutorialId = 1, Video = "vid"});
            modelBuilder.Entity<Step>().HasData(new Step() {Id = 3, StepNumber = 3, TutorialId = 1, Video = "vid"});

            modelBuilder.Entity<StepLanguage>().HasData(new StepLanguage()
                {Id = 1,  StepId = 1, Text = "Sleep at least 8 hours a day", TutorialLanguageId = 1, Title = "Sleep well", Audio = "aud"});
            modelBuilder.Entity<StepLanguage>().HasData(new StepLanguage()
                {Id = 2, StepId = 2, Text = "Drink 2 liters of water daily", TutorialLanguageId = 1, Title = "Drink water", Audio = "aud"});
            modelBuilder.Entity<StepLanguage>().HasData(new StepLanguage()
                {Id = 3, StepId = 3, Text = "Eat healthy food 3 times a day", TutorialLanguageId = 1, Title = "Eat healthy", Audio = "aud"});
        }

    }
}