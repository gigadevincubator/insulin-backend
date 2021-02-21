using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;

namespace insulin_backend.Services.User
{
    public class UserService : IUserService
    {
        private DataContext dbContext { get; set; }

        public UserService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<TutorialLanguage> GetAllUsersTutorials(int userId)
        {
            // IList<TutorialLanguage> tutorials =
            //     dbContext.Users.First(u => u.Id == userId).Tutorials.ToList();

            IList<TutorialLanguage> tutorials =
                dbContext.Users.Where(u => u.Id == userId).SelectMany(u => u.Tutorials).Include(t => t.Tutorial)
                    .Include(u => u.User).ToList();
            if (!tutorials.Any())
            {
                throw new NotFoundException();
            }

            return tutorials;
        }
    }
}