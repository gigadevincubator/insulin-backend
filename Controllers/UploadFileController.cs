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

        public UploadFileController(IAzureBlobService azureBlobService)
        {
            _azureBlobService = azureBlobService;
        }


        [HttpPost]
        public async Task<ActionResult> UploadAsync()
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

                await _azureBlobService.UploadAsync(files);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }
    }
}