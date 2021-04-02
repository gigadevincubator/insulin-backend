using System;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace insulin_backend.Database.Repository.Extended
{
    public class TutorialRepository : Repository<Tutorial>, ITutorialRepository
    {
        private readonly DataContext _dataContext;
        public TutorialRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Tutorial CreateTutorial(Tutorial tutorial)
        {
            try
            {
                Console.WriteLine("Color:" + tutorial.Color + "," + tutorial.ThumbnailUrl + "," + tutorial.isPublished + "," + tutorial.Id);
               _dataContext.Tutorials.Add(tutorial);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return tutorial;
        }
        
        public Tutorial FindTutorialById(int tutorialId)
        {
            var tutorial =  _dataContext.Tutorials.FirstOrDefault(item => item.Id == tutorialId);
            if (tutorial == null)
            {
                throw new Exception("Tutorial not found");
            }

            return tutorial;
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}