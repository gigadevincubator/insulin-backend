using insulin_backend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace insulin_backend.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<StepLanguage> StepLanguage { get; set; }
        public DbSet<TutorialLanguage> TutorialLanguages { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Insulin.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasMany<Tutorial>(p => p.Tutorials).WithMany(t => t.Patients);

        }

    }
}