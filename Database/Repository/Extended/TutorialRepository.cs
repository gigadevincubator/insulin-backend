using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public class TutorialRepository : Repository<Tutorial>, ITutorialRepository
    {
        public TutorialRepository(DataContext context) : base(context)
        {
            
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}