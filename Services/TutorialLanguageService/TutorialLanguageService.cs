using System;
using System.Linq;
using insulin_backend.Database;
using insulin_backend.Services.Exceptions;

namespace insulin_backend.Services.TutorialLanguageService
{
    public class TutorialLanguageService : ITutorialLanguageSerive
    {
        private DataContext dbContext { get; set; }

        public TutorialLanguageService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Object GetTutorialLanguageByTitle(string title, int languageId)
        {
            try
            {
                var tutorial =
                    from tl in dbContext.TutorialLanguages
                    join t in dbContext.Tutorials on tl.TutorialId equals t.Id
                    join s in dbContext.Steps on t.Id equals s.TutorialId
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
    }
}
