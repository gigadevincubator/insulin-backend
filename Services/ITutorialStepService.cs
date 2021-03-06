using System.Threading.Tasks;
using insulin_backend.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Services
{
    public interface ITutorialStepService
    {
        public StepLanguage GetStepLanguage(int tutorialId, int stepNr, int languageId);
        StepLanguage FindStepLanguageById(int stepLanguageId);
        Step FindStepById(int stepId);
        Task DeleteTutorialStepAsync(int tutorialId, int stepId, int languageId);
    }
}