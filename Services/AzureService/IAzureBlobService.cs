using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace insulin_backend.Services.AzureService
{
    public interface IAzureBlobService
    {
        

        Task UploadStepVideoAsync(IFormFileCollection files, int stepVideoId);
        Task UploadStepLanguageAudioAsync(IFormFileCollection files, int objectId);
        Task UploadTutorialThumbnailAsync(IFormFileCollection files, int tutorialThumbnailId
        );
    }
}