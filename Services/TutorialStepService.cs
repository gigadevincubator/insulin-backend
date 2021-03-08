
using System;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace insulin_backend.Services
{
    public class TutorialStepService : ITutorialStepService
    {
        private DataContext dbContext { get; set; }
        
        public TutorialStepService(DataContext dbContext)
        {
            this.dbContext = dbContext;
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
                    (from sl in dbContext.StepLanguage
                        join s in dbContext.Steps on sl.StepId equals s.Id
                        join tl in dbContext.TutorialLanguages on sl.TutorialLanguageId equals tl.Id
                        where tl.TutorialId == tutorialId && tl.LanguageId == languageId && s.TutorialId == tutorialId && s.StepNumber == stepNr
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
    }
}