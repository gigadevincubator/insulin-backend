using System;
using insulin_backend.Database.Repository.Extended;

namespace insulin_backend.Database.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITutorialRepository Tutorials { get; }
        IPatientRepository Patients { get;  }
        ILanguageRepository Languages { get; }
        IDoctorRepository Doctors { get; }
        int Complete();
    }
}