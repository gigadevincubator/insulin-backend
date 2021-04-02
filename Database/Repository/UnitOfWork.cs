using insulin_backend.Database.Models;
using insulin_backend.Database.Repository.Extended;

namespace insulin_backend.Database.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Tutorials = new TutorialRepository(_context);
            Users = new UserRepository(_context);
            TutorialSteps = new TutorialStepRepository(_context);
            Steps = new StepRepository(_context);
            TutorialLanguages = new TutorialLanguageRepository(_context);
            StepLanguages = new StepLanguageRepository(_context);
        }
        public ITutorialLanguageRepository TutorialLanguages { get; }
        public ITutorialRepository Tutorials { get; }
        public IUserRepository Users { get; }
        public ITutorialStepRepository TutorialSteps { get; }
        public IStepLanguageRepository StepLanguages { get; }
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