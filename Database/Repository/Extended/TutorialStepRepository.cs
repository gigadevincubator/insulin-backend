using System;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;

namespace insulin_backend.Database.Repository.Extended
{
    public class TutorialStepRepository : Repository<Step>, ITutorialStepRepository
    {
        
        private readonly DataContext _dataContext;
        
        public TutorialStepRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public async Task DeleteTutorialStepAsync(int tutorialId, int stepId, int languageId)
        {
            
            var tutorial =  _dataContext.Tutorials.FirstOrDefault(t => t.Id == tutorialId);
            if (tutorial == null)
            {
                throw new Exception("Tutorial not found");
            }

            var tutorialSteps = _dataContext.Steps.Where(s => s.TutorialId == tutorial.Id)
                .ToList();

            if (tutorialSteps == null)
            {
                throw new Exception("Tutorial Steps not found");
            }

            var tutorialStepsLanguage =  _dataContext.StepLanguage.Where(sl => sl.Id == languageId).ToList();
            //Remove tutorial language steps
            foreach (var tutorialStepLang in tutorialStepsLanguage)
            {
                _dataContext.Remove(tutorialStepLang);
                _dataContext.SaveChanges();

            }
            for (int i = 0; i < tutorialSteps.Count; i++)
            {
                // Remove tutorial step and tutorial language 
                if (tutorialSteps[i].Id == stepId)
                {
                    var stepNumberTemp = tutorialSteps[i].StepNumber;
                    _dataContext.Remove(tutorialSteps[i]);
                    // Decrement the tutorial number
                    for (int j = 0; j < tutorialSteps.Count; j++)
                    {
                        if (tutorialSteps[j].StepNumber > stepNumberTemp)
                        {
                            tutorialSteps[j].StepNumber = --tutorialSteps[j].StepNumber;
                        }
                    }
                }
            }
        }
        
        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}