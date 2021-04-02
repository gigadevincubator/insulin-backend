using System;
using insulin_backend.Database.Models;
using insulin_backend.Database.Repository;
using insulin_backend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    public class TutorialController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        
        public TutorialController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        [Route("tutorials/create")]
        public ActionResult<Tutorial> CreateTutorial([FromBody] Tutorial tutorialObj)
        {
            try
            {
                Tutorial tutorial = _unitOfWork.Tutorials.CreateTutorial(new Tutorial()
                {
                    Color = tutorialObj.Color,
                    ThumbnailUrl = tutorialObj.ThumbnailUrl,
                    isPublished = tutorialObj.isPublished,
                });
                Console.WriteLine("Color: " + tutorial.Color);
                _unitOfWork.Complete();
                return Ok(tutorial);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}