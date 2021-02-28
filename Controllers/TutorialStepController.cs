using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using insulin_backend.Database.Models;
using insulin_backend.Services;
using insulin_backend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    public class TutorialStepController : ControllerBase
    {
        private ITutorialStepService _tutorialStepService;

        public TutorialStepController(ITutorialStepService tutorialStepService)
        {
            _tutorialStepService = tutorialStepService;
        }

        [HttpGet]
        [Route("tutorials/{tutorialId:int}/steps/{stepNr:int}")]
        public ActionResult<Step> GetStep([FromRoute] int tutorialId, [FromRoute] int stepNr,
            [FromQuery] int languageId)
        {
            try
            {
                StepLanguage res = _tutorialStepService.GetStepLanguage(tutorialId, stepNr, languageId);
                return Ok(res);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("DELETE tutorials/{tutorialId}/delete")]
        public ActionResult<int> DeleteTutorial([FromRoute] int tutorialId, [FromQuery] string language)
        {
            try
            {
                return Ok(_tutorialStepService.DeleteStepOfTutorial(tutorialId,language));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
    }
}