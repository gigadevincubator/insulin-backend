using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public interface ITutorialStepRepository
    {
        Step CreateStep(Step step);
    }
}