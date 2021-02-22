using System;
using System.Collections;
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

        public Object GetAllUsersTutorials(int userId)
        {
            // final object that must be returned 
            ArrayList tutorialToReturn = new ArrayList();

            // Join three tables: Users, TutorialLanguages, Tutorials 
            var fetchedTutorialData =
                from u in dbContext.Users
                join tl in dbContext.TutorialLanguages on u.Id equals tl.UserId
                join t in dbContext.Tutorials on tl.TutorialId equals t.Id
                where tl.UserId == userId
                select new
                {
                    tutorialId = tl.Id,
                    tutorialTitle = tl.Title,
                    tutorialColor = t.Color,
                };
            // Check if the fetched object is null and throw an exception, caused if the user id is not found in the database
            if (fetchedTutorialData == null)
            {
                throw new NotFoundException();
            }

            // Append to each tutorial from the fetchedTutorialData object the number of steps.
            foreach (var tutorial in fetchedTutorialData)
            {
                var numberOfSteps = CountNumberOfSteps(tutorial.tutorialId);
                tutorialToReturn.Add(new
                {
                    tutorial.tutorialId,
                    tutorial.tutorialTitle,
                    tutorial.tutorialColor,
                    numberOfSteps
                });
            }

            return tutorialToReturn;
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