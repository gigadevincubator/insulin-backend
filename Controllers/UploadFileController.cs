using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using insulin_backend.Services.AzureService;
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
        private const string TutorialThumbnail = "TutorialThumbnail";
        private const string StepVideo = "StepVideo";
        private const string StepLanguageAudio = "StepLanguageAudio";

        public UploadFileController(IAzureBlobService azureBlobService)
        {
            _azureBlobService = azureBlobService;
        }


        [HttpPost("tutorialThumbnail/{tutorialId}")]
        //todo check if tutorial exists
        public async Task<ActionResult> UploadTutorialThumbnailAsync([FromRoute] int tutorialId)
        {
            return await UploadFileAsync(TutorialThumbnail,tutorialId);
        }

        [HttpPost("stepVideo/{stepId}")]
        public async Task<ActionResult> UploadStepVideoAsync([FromRoute] int stepId)
        {
            return await UploadFileAsync(StepVideo,stepId);
        }

        [HttpPost("stepLanguageAudio/{stepLanguageId}")]
        public async Task<ActionResult> UploadStepLanguageAudioAsync([FromRoute] int stepLanguageId)
        {
            return await UploadFileAsync(StepLanguageAudio,stepLanguageId);
        }

        private async Task<ActionResult> UploadFileAsync(string objectToUpload,int objectId)
        {
            try
            {
                var request = await HttpContext.Request.ReadFormAsync();
                if (request.Files == null)
                {
                    return BadRequest("Could not upload files");
                }

                var files = request.Files;
                if (files.Count == 0)
                {
                    return BadRequest("Could not upload empty files");
                }

                switch (objectToUpload)
                {
                    case StepVideo:  await _azureBlobService.UploadStepVideoAsync(files, objectId);
                    break;    
                    case StepLanguageAudio: await _azureBlobService.UploadStepLanguageAudioAsync(files, objectId);
                    break;
                    case TutorialThumbnail:await _azureBlobService.UploadTutorialThumbnailAsync(files, objectId);
                    break;
                }
          
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}