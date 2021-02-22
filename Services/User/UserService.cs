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
        private DataContext dbContext;

        public UserService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

//todo this can be optimized using only one query or using LINQ include statement 
        public async Task<Database.Models.User> GetAllUsersTutorials(int userId)
        {
// Find the user based on the user ID
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

// Load All related child entities of the user
            await dbContext.Entry(user).Collection(p => p.Tutorials).LoadAsync();
            foreach (var tutorial in user.Tutorials)
            {
                await dbContext.Entry(tutorial).Reference(p => p.Tutorial).LoadAsync();
                await dbContext.Entry(tutorial).Reference(p => p.Language).LoadAsync();
            }
            
            return user;
        }
    }
}