using System;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;
using insulin_backend.Services.TutorialLanguageService;
using insulin_backend.Services.TutorialService;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    
        [ApiController]
        public class TutorialLanguageController : ControllerBase
        {
            private ITutorialLanguageService _tutorialLanguageService;
        private ITutorialService tutorialService;

        public TutorialLanguageController(ITutorialLanguageService tutorialLanguageService)
            {
                this._tutorialLanguageService = tutorialLanguageService;
            }

            [HttpGet]
            [Route("tutorials")]
            public ActionResult<TutorialLanguage> GetStep([FromQuery] string title, [FromQuery] int languageId)
            {
                try
                {
                    Object res = _tutorialLanguageService.GetTutorialLanguageByTitle(title,languageId);
                    return Ok(res);
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }
            }

        [HttpPut]
        [Route("tutorials/{tutorial_id:int}/languages/{language_id:int}/edit")]
        public ActionResult<Object> UpdateTutorial([FromBody] string jsonData, int tutorial_id, int language_id)
        {
            try
            {
                Object res = tutorialService.UpdateTutorial(jsonData ,tutorial_id,language_id);
                return Ok(res);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
    }

