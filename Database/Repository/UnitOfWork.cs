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
        }
        
        public ITutorialRepository Tutorials { get; }
        public IUserRepository Users { get; }
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