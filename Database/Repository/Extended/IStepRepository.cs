using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public interface IStepRepository : IRepository<Step>
    {
        Step CreateStep(Step step);
    }
}