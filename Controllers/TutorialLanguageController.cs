using System;
using insulin_backend.Database.Models;
using insulin_backend.Database.Repository;
using insulin_backend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    
    [ApiController]
    public class TutorialLanguageController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public TutorialLanguageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("tutorials")]
        public ActionResult<TutorialLanguage> GetTutorials([FromQuery] string title, [FromQuery] int languageId)
        {
            try
            {
                Object res = _unitOfWork.TutorialLanguages.GetTutorialLanguageByTitle(title,languageId);
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
                Object res = _unitOfWork.TutorialLanguages.UpdateTutorial(jsonData ,tutorial_id,language_id);
                return Ok(res);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
    }

