using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;
using insulin_backend.Services.UserService;

namespace insulin_backend.Services.UserService
{
    public class UserService : IUserService
    {
        private DataContext dbContext;

        public UserService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Object GetAllUsersTutorials(int userId)
        {
            // final object that must be returned 
            ArrayList tutorialToReturn = new ArrayList();
            var fetchedUser=GetUserByUserId(userId);
            if (fetchedUser == null)
            {
                throw new NotFoundException("User not found");
            }
            // Join three tables: Users, TutorialLanguages, Tutorials 
            var fetchedTutorialData =
                from u in dbContext.Users
                join tl in dbContext.TutorialLanguages on u.Id equals tl.UserId
                join t in dbContext.Tutorials on tl.TutorialId equals t.Id
                where tl.UserId == userId
                select new
                {
                    id = tl.Id,
                    title = tl.Title,
                    color = t.Color,
                    language=tl.Language.Name
                };
            
            // Append to each tutorial from the fetchedTutorialData object the number of steps.
            foreach (var tutorial in fetchedTutorialData)
            {
                var amountOfSteps = CountNumberOfSteps(tutorial.id);
                tutorialToReturn.Add(new
                {
                    tutorial.id,
                    tutorial.title,
                    tutorial.color,
                    amountOfSteps,
                    tutorial.language
                });
            }

            return tutorialToReturn;
        }

        private User GetUserByUserId( int userId)
        {
            return dbContext.Users.FirstOrDefault(u=>u.Id==userId);
        }

        private int CountNumberOfSteps(int tutorialId)
        {
            return
                (from s in dbContext.Steps
                    where s.TutorialId == tutorialId
                    select s).Count();
        }
    }
}