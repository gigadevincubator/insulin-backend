using System;
using insulin_backend.Database.Repository.Extended;

namespace insulin_backend.Database.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITutorialRepository Tutorials { get; }
        IUserRepository Users { get;  }
        ILanguageRepository Languages { get; }
        int Complete();
    }
}