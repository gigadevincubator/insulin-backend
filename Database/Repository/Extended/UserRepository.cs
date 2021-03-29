using System;
using System.Collections;
using System.Linq;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;

namespace insulin_backend.Database.Repository.Extended
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext context) : base(context)
        {
            _dataContext = context;
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
                from u in _dataContext.Users
                join tl in _dataContext.TutorialLanguages on u.Id equals tl.UserId
                join t in _dataContext.Tutorials on tl.TutorialId equals t.Id
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
            return _dataContext.Users.FirstOrDefault(u=>u.Id==userId);
        }

        private int CountNumberOfSteps(int tutorialId)
        {
            return
                (from s in _dataContext.Steps
                    where s.TutorialId == tutorialId
                    select s).Count();
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}