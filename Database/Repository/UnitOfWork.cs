using insulin_backend.Database.Models;
using insulin_backend.Database.Repository.Extended;

namespace insulin_backend.Database.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork()
        {}

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Tutorials = new TutorialRepository(_context);
            Users = new UserRepository(_context);
            Languages = new LanguageRepository(_context);
            TutorialSteps = new TutorialStepRepository(_context);
            Steps = new StepRepository(_context);
        }
        
        public ITutorialRepository Tutorials { get; }
        public IUserRepository Users { get; }
        public ILanguageRepository Languages { get; }
        public ITutorialStepRepository TutorialSteps { get; }

        public IStepRepository Steps { get; }

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