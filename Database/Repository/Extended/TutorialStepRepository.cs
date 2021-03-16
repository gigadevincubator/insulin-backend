using System;
using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public class TutorialStepRepository : Repository<Step>, ITutorialStepRepository
    {
        
        private readonly DataContext _dataContext;
        
        public TutorialStepRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Step CreateStep(Step step)
        {
            try
            {
                _dataContext.Steps.Add(step);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return step;
        }
        
        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}