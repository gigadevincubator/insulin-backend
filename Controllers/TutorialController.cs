using insulin_backend.Services.TutorialService;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    public class TutorialController  : ControllerBase
    {
        private ITutorialService tutorialService;

        public TutorialController(ITutorialService tutorialService)
        {
            this.tutorialService = tutorialService;
        }
       
        
    }
}