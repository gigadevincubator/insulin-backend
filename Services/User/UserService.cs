using System;
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
        public Object GetAllUsersTutorials(int userId)
        {
// // Find the user based on the user ID
//             var user = await dbContext.Users.FindAsync(userId);
//             if (user == null)
//             {
//                 throw new NotFoundException();
//             }
//
// // Load All related child entities of the user
//             await dbContext.Entry(user).Collection(p => p.Tutorials).LoadAsync();
//             foreach (var tutorial in user.Tutorials)
//             {
//                 await dbContext.Entry(tutorial).Reference(p => p.Tutorial).LoadAsync();
//                 await dbContext.Entry(tutorial).Reference(p => p.Language).LoadAsync();
//             }

            var step =
                (from u in dbContext.Users
                    join tl in dbContext.TutorialLanguages on u.Id equals tl.UserId
                    join t in dbContext.Tutorials on tl.TutorialId equals t.Id
                    join s in dbContext.Steps on t.Id equals s.Id
                    where tl.UserId == userId
                    select new
                    {
                        t=t,tl=tl,s=s
                      
                    });
            return step;
         
        }
    }
}