using System;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database;
using insulin_backend.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using insulin_backend.Services.Exceptions;

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

        public async Task<Tutorial> UpdateTutorial(string jsonData, int tutorialId, int languageId)
        {
            try {
                var jsonTutorial = JsonConvert.DeserializeObject<Tutorial>(jsonData);
                var jsonTutorialLanguage = JsonConvert.DeserializeObject<TutorialLanguage>(jsonData);

                Tutorial toUpdateTutorial = await dbContext.Tutorials.FirstAsync(t => t.Id == tutorialId);
                TutorialLanguage toUpdateTutorialLanguage = await dbContext.TutorialLanguages.FirstAsync(tl => tl.TutorialId == tutorialId && tl.LanguageId == languageId);


                toUpdateTutorial.isPublished = jsonTutorial.isPublished;
                toUpdateTutorial.ThumbnailUrl = jsonTutorial.ThumbnailUrl;
                toUpdateTutorialLanguage.Title = jsonTutorialLanguage.Title;
                dbContext.Update(toUpdateTutorial);
                dbContext.Update(toUpdateTutorialLanguage);
                await dbContext.SaveChangesAsync();
                return toUpdateTutorial;
            }
            catch (InvalidOperationException e)
            {
                throw new NotFoundException();
            }



        }
    }
}