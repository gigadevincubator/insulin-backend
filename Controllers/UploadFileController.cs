using System;
using System.Threading.Tasks;
using insulin_backend.Database.Repository;
using insulin_backend.Services.AzureService;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadFileController : ControllerBase
    {
        private readonly IAzureBlobService _azureBlobService;

        private readonly IUnitOfWork _unitOfWork;
        private const string TutorialThumbnail = "TutorialThumbnail";
        private const string StepVideo = "StepVideo";
        private const string StepLanguageAudio = "StepLanguageAudio";

        public UploadFileController(IAzureBlobService azureBlobService, IUnitOfWork unitOfWork)
        {
            _azureBlobService = azureBlobService;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("tutorialThumbnail/{tutorialId}")]
        public async Task<ActionResult> UploadTutorialThumbnailAsync([FromRoute] int tutorialId)
        {
            try
            {
                 _unitOfWork.Tutorials.FindTutorialById(tutorialId);
                await UploadFileAsync(TutorialThumbnail, tutorialId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("stepVideo/{stepId}")]
        public async Task<ActionResult> UploadStepVideoAsync([FromRoute] int stepId)
        {
            try
            {
                 _unitOfWork.Steps.FindStepById(stepId);
                await UploadFileAsync(StepVideo, stepId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("stepLanguageAudio/{stepLanguageId}")]
        public async Task<ActionResult> UploadStepLanguageAudioAsync([FromRoute] int stepLanguageId)
        {
            try
            {
                 _unitOfWork.StepLanguages.FindStepLanguageById(stepLanguageId);
                await UploadFileAsync(StepLanguageAudio, stepLanguageId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        private async Task UploadFileAsync(string objectToUpload, int objectId)
        {
            var request = await HttpContext.Request.ReadFormAsync();
            if (request.Files == null)
            {
                throw new Exception("Could not upload files");
            }

            var files = request.Files;
            if (files.Count == 0)
            {
                throw new Exception("Could not upload files");
            }

            switch (objectToUpload)
            {
                case StepVideo:
                    await _azureBlobService.UploadStepVideoAsync(files, objectId);
                    break;
                case StepLanguageAudio:
                    await _azureBlobService.UploadStepLanguageAudioAsync(files, objectId);
                    break;
                case TutorialThumbnail:
                    await _azureBlobService.UploadTutorialThumbnailAsync(files, objectId);
                    break;
            }
        }
    }
}