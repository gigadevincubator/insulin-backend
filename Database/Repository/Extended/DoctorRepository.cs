using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DataContext context) : base(context)
        {
            
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}