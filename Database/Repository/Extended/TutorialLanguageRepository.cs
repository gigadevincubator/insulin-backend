using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace insulin_backend.Database.Repository.Extended
{
    public class TutorialLanguageRepository : Repository<TutorialLanguage>, ITutorialLanguageRepository
    {
        private readonly DataContext _dataContext;
        
        public TutorialLanguageRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Object GetTutorialLanguageByTitle(string title, int languageId)
        {
            try
            {
                var tutorial =
                    from tl in _dataContext.TutorialLanguages
                    join t in _dataContext.Tutorials on tl.TutorialId equals t.Id
                    join s in _dataContext.Steps on t.Id equals s.TutorialId
                    where tl.Title.ToLower().Contains(title.ToLower()) && tl.LanguageId == languageId
                    select new
                    {
                        TutorialId = t.Id,
                        Title = tl.Title,
                        Color = t.Color,
                        TutorialLanguage = tl.Language.Name,
                    };
                return tutorial;
            }
            catch (InvalidOperationException e)
            {
                throw new NotFoundException();
            }
        }
        
        public async Task<Tutorial> UpdateTutorial(string jsonData, int tutorialId, int languageId)
        {
            try {
                var jsonTutorial = JsonConvert.DeserializeObject<Tutorial>(jsonData);
                var jsonTutorialLanguage = JsonConvert.DeserializeObject<TutorialLanguage>(jsonData);

                Tutorial toUpdateTutorial = await _dataContext.Tutorials.FirstAsync(t => t.Id == tutorialId);
                TutorialLanguage toUpdateTutorialLanguage = await _dataContext.TutorialLanguages.FirstAsync(tl => tl.TutorialId == tutorialId && tl.LanguageId == languageId);


                toUpdateTutorial.isPublished = jsonTutorial.isPublished;
                toUpdateTutorial.ThumbnailUrl = jsonTutorial.ThumbnailUrl;
                toUpdateTutorialLanguage.Title = jsonTutorialLanguage.Title;
                _dataContext.Update(toUpdateTutorial);
                _dataContext.Update(toUpdateTutorialLanguage);
                await _dataContext.SaveChangesAsync();
                return toUpdateTutorial;
            }
            catch (InvalidOperationException e)
            {
                throw new NotFoundException();
            }
        }
        
        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}