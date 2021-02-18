using insulin_backend.Database.Models;
using insulin_backend.Database.Repository.Extended;

namespace insulin_backend.Database.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Tutorials = new TutorialRepository(_context);
            Doctors = new DoctorRepository(_context);
            Patients = new PatientRepository(_context);
            Languages = new LanguageRepository(_context);
        }
        
        public ITutorialRepository Tutorials { get; }
        public IDoctorRepository Doctors { get; }
        public IPatientRepository Patients { get; }
        public ILanguageRepository Languages { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}