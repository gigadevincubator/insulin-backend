using System.Threading.Tasks;
using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public interface ITutorialRepository : IRepository<Tutorial>
    {
        Tutorial CreateTutorial(Tutorial tutorial);
        Tutorial FindTutorialById(int tutorialId);
    }
}