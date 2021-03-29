using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public interface IStepLanguageRepository : IRepository<StepLanguage>
    {
        StepLanguage GetStepLanguage(int tutorialId, int stepNr, int languageId);
        StepLanguage FindStepLanguageById(int stepLanguageId);
    }
}