using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public LanguageRepository(DataContext context) : base(context)
        {
            
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}