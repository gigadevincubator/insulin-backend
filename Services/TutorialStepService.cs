using System;
using System.Data.Entity;
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
            var stepLanguage = dbContext.StepLanguage.FirstOrDefault(item => item.Id == stepLanguageId);
            if (stepLanguage == null)
            {
                throw new Exception("Step Language not found");
            }

            return stepLanguage;
        }

        public Step FindStepById(int stepId)
        {
            var step = dbContext.Steps.FirstOrDefault(item => item.Id == stepId);
            if (step == null)
            {
                throw new Exception("Step  not found");
            }

            return step;
        }

        public async Task DeleteTutorialStepAsync(int tutorialId, int stepId, int languageId)
        {
            
            var tutorial =  dbContext.Tutorials.FirstOrDefault(t => t.Id == tutorialId);
            if (tutorial == null)
            {
                throw new Exception("Tutorial not found");
            }

            var tutorialSteps =  dbContext.Steps.Where(s => s.TutorialId == tutorial.Id)
                .ToList();

            if (tutorialSteps == null)
            {
                throw new Exception("Tutorial Steps not found");
            }

            var tutorialStepsLanguage =  dbContext.StepLanguage.Where(sl => sl.Id == languageId).ToList();
//Remove tutorial language steps
            foreach (var tutorialStepLang in tutorialStepsLanguage)
            {
                dbContext.Remove(tutorialStepLang);
                dbContext.SaveChanges();

            }
            for (int i = 0; i < tutorialSteps.Count; i++)
            {
                // Remove tutorial step and tutorial language 
                if (tutorialSteps[i].Id == stepId)
                {
                    var stepNumberTemp = tutorialSteps[i].StepNumber;
                    dbContext.Remove(tutorialSteps[i]);
// Decrement the tutorial number
                    for (int j = 0; j < tutorialSteps.Count; j++)
                    {
                        if (tutorialSteps[j].StepNumber > stepNumberTemp)
                        {
                            tutorialSteps[j].StepNumber = --tutorialSteps[j].StepNumber;
                            dbContext.SaveChanges();
                        }
                    }

                     dbContext.SaveChanges();
                }
            }
        }
    }
}