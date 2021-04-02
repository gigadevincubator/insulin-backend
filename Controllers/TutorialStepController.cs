using System;
using System.Threading.Tasks;
using insulin_backend.Database.Models;
using insulin_backend.Database.Repository;
using insulin_backend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    public class TutorialStepController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public TutorialStepController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("tutorials/{TutorialId}/steps/{stepNr}")]
        public ActionResult<Step> GetStep([FromRoute] int tutorialId, [FromRoute] int stepNr,
            [FromQuery] int languageId)
        {
            try
            {
                StepLanguage res = _unitOfWork.StepLanguages.GetStepLanguage(tutorialId, stepNr, languageId);
                return Ok(res);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("tutorials/{TutorialId}/steps/{stepId}/{languageId}")]
        public ActionResult<Task> DelegateTutorialStep([FromRoute] int tutorialId,[FromRoute] int stepId, [FromRoute] int languageId)
        {
            try
            {
                 _unitOfWork.TutorialSteps.DeleteTutorialStepAsync(tutorialId,stepId,languageId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        [Route("tutorials/{TutorialId}/steps/{stepNr}/create")]
        public ActionResult<Step> CreateTutorialStep([FromRoute] int tutorialId, [FromRoute] int stepNr, [FromBody] Step stepObj)
        {
            try
            {
                Step step = _unitOfWork.Steps.CreateStep(new Step()
                {
                    TutorialId = tutorialId,
                    StepNumber = stepNr,
                    VideoUrl = stepObj.VideoUrl,
                    Audio = stepObj.Audio,
                    Title = stepObj.Title,
                    Text = stepObj.Text
                });
                return Ok(step);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}