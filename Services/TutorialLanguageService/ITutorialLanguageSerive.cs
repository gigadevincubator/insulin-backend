using System;

namespace insulin_backend.Services.TutorialLanguageService
{
    public interface ITutorialLanguageSerive
    {
        public Object GetTutorialLanguageByTitle(string title, int languageId);
    }
}
