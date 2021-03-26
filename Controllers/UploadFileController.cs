using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using insulin_backend.Services;
using insulin_backend.Services.AzureService;
using insulin_backend.Services.TutorialLanguageService;
using insulin_backend.Services.TutorialService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Logging;

namespace insulin_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadFileController : ControllerBase
    {
        private readonly IAzureBlobService _azureBlobService;

        private readonly ITutorialStepService _tutorialStepService;
        private readonly ITutorialService _tutorialService;
        private const string TutorialThumbnail = "TutorialThumbnail";
        private const string StepVideo = "StepVideo";
        private const string StepLanguageAudio = "StepLanguageAudio";

        public UploadFileController(IAzureBlobService azureBlobService,
           ITutorialStepService tutorialStepService,
            ITutorialService tutorialService)
        {
            _azureBlobService = azureBlobService;
            _tutorialStepService = tutorialStepService;
            _tutorialService = tutorialService;
        }


        [HttpPost("tutorialThumbnail/{tutorialId}")]
        public async Task<ActionResult> UploadTutorialThumbnailAsync([FromRoute] int tutorialId)
        {
            try
            {
                 _tutorialService.FindTutorialById(tutorialId);
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
                 _tutorialStepService.FindStepById(stepId);
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
                 _tutorialStepService.FindStepLanguageById(stepLanguageId);
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