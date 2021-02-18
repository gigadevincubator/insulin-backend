using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(DataContext context) : base(context)
        {
            
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}