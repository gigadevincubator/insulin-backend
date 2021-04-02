using System;
using insulin_backend.Database.Repository.Extended;

namespace insulin_backend.Database.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITutorialRepository Tutorials { get; }
        IUserRepository Users { get;  }
        IStepRepository Steps { get; }
        ITutorialLanguageRepository TutorialLanguages { get; }
        IStepLanguageRepository StepLanguages { get; }
        ITutorialStepRepository TutorialSteps { get; }
        int Complete();
    }
}