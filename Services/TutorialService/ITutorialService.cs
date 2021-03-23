using System;
using System.Threading.Tasks;
using insulin_backend.Database.Models;

namespace insulin_backend.Services.TutorialService
{
    public interface ITutorialService
    {
        Tutorial FindTutorialById(int tutorialId);

        Task<Tutorial> UpdateTutorial(string jsonData, int tutorialId, int languageId);
    }
}