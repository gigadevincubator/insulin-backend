using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;
using insulin_backend.Services.TutorialLanguageService;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    
        [ApiController]
        public class TutorialLanguageController : ControllerBase
        {
            private ITutorialLanguageService _tutorialLanguageService;

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
        }
    }

