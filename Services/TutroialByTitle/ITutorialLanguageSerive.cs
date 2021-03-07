using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database.Models;

namespace insulin_backend.Services.TutroialByTitle
{
    public interface ITutorialLanguageSerive
    {
        public Object GetTutorialLanguageByTitle(string title, int languageId);
    }
}
