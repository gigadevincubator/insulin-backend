using insulin_backend.Database.Models;
using insulin_backend.Database.Repository;
using insulin_backend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public StepController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        [Route("tutorials/{tutorialId}/steps/{stepNr}/create")]
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
                _unitOfWork.Complete();
                return Ok(step);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}