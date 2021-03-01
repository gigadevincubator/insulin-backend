using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace insulin_backend.Services.AzureService
{
    public interface IAzureBlobService
    {
        Task UploadAsync(IFormFileCollection files);

    }
}