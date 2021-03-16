using insulin_backend.Database.Models;
using insulin_backend.Database.Repository;
using insulin_backend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    public class StepController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        
        public StepController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        [Route("tutorials/steps/{stepNr:int}/create")]
        public ActionResult<Step> CreateStep([FromRoute] int stepNr, [FromBody] string video, [FromBody] string audio, [FromBody] string title, [FromBody] string text)
        {
            try
            {
                Step step = _unitOfWork.Steps.CreateStep(new Step()
                {
                    StepNumber = stepNr,
                    Video = video,
                    Audio = audio,
                    Title = title,
                    Text = text
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