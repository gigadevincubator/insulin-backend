using System;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database;
using insulin_backend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace insulin_backend.Services.TutorialService
{
    public class TutorialService :ITutorialService
    {
        private DataContext dbContext { get; set; }
        
        public TutorialService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Tutorial FindTutorialById(int tutorialId)
        {
            var tutorial =  dbContext.Tutorials.FirstOrDefault(item => item.Id == tutorialId);
            if (tutorial == null)
            {
                throw new Exception("Tutorial not found");
            }

            return tutorial;
        }
    }
}