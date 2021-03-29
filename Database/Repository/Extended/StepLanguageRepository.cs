using System;
using System.Linq;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;

namespace insulin_backend.Database.Repository.Extended
{
    public class StepLanguageRepository : Repository<StepLanguage>, IStepLanguageRepository
    {

        private readonly DataContext _dataContext;
        public StepLanguageRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
        
        public StepLanguage GetStepLanguage(int tutorialId, int stepNr, int languageId)
        {
            /*
             select * from StepLanguage sl join step s on sl.step_id = s.id
             join TutorialLanguage tl on sl.tutorial_language_id = tl.id
             where tl.tutorial_id = #{tutorialId} and tl.languageId = #{languageId} and s.tutorial_id = #{tutorialId} and s.stepNr = #{stepNr}
             */
            try
            {
                StepLanguage step =
                    (from sl in _dataContext.StepLanguage
                        join s in _dataContext.Steps on sl.StepId equals s.Id
                        join tl in _dataContext.TutorialLanguages on sl.TutorialLanguageId equals tl.Id
                        where tl.TutorialId == tutorialId && tl.LanguageId == languageId &&
                              s.TutorialId == tutorialId && s.StepNumber == stepNr
                        select new StepLanguage()
                        {
                            Id = sl.Id,
                            Title = sl.Title,
                            StepId = sl.StepId,
                            Text = sl.Text,
                            AudioUrl = sl.AudioUrl,
                            TutorialLanguageId = sl.TutorialLanguageId
                        }).First();
                return step;
            }
            catch (InvalidOperationException e)
            {
                throw new NotFoundException();
            }
        }

        public StepLanguage FindStepLanguageById(int stepLanguageId)
        {
            var stepLanguage = _dataContext.StepLanguage.FirstOrDefault(item => item.Id == stepLanguageId);
            if (stepLanguage == null)
            {
                throw new Exception("Step Language not found");
            }

            return stepLanguage;
        }

        
        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}