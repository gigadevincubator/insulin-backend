using System;
using System.Threading.Tasks;
using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public interface ITutorialLanguageRepository : IRepository<TutorialLanguage>
    {
        public Object GetTutorialLanguageByTitle(string title, int languageId);
        Task<Tutorial> UpdateTutorial(string jsonData, int tutorialId, int languageId);
    }
}