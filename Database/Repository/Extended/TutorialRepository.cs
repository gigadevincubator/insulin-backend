using System;
using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public class TutorialRepository : Repository<Tutorial>, ITutorialRepository
    {
        private readonly DataContext _dataContext;
        public TutorialRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Tutorial CreateTutorial(Tutorial tutorial)
        {
            try
            {
               _dataContext.Tutorials.Add(tutorial);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return tutorial;
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}