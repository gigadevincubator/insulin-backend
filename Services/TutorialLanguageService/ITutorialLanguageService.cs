using System;
using System.Threading.Tasks;
using insulin_backend.Database.Models;

namespace insulin_backend.Services.TutorialLanguageService
{
    public interface ITutorialLanguageService
    {
        public Object GetTutorialLanguageByTitle(string title, int languageId);

    }
}
