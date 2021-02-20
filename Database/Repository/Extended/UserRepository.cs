using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
            
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}