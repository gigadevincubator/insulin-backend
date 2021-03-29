using System.Threading.Tasks;
using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public interface ITutorialStepRepository
    {
        Task DeleteTutorialStepAsync(int tutorialId, int stepId, int languageId);
    }
}