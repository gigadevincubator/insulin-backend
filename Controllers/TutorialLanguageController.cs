using System;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;
using insulin_backend.Services.TutroialByTitle;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    
        [ApiController]
        public class TutorialLanguageController : ControllerBase
        {
            private ITutorialLanguageSerive tutorialLanguageSerive;

            public TutorialLanguageController(ITutorialLanguageSerive tutorialLanguageSerive)
            {
                this.tutorialLanguageSerive = tutorialLanguageSerive;
            }

            [HttpGet]
            [Route("tutorials")]
            public ActionResult<TutorialLanguage> GetStep([FromQuery] string title, [FromQuery] int languageId)
            {
                try
                {
                    Object res = tutorialLanguageSerive.GetTutorialLanguageByTitle(title,languageId);
                    return Ok(res);
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }
            }
        }
    }

