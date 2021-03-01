using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace insulin_backend.Services.AzureService
{
    public class AzureBlobService :IAzureBlobService
    {
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;

		public AzureBlobService(IAzureBlobConnectionFactory azureBlobConnectionFactory)
		{
			_azureBlobConnectionFactory = azureBlobConnectionFactory;
		}
		

		public async Task UploadAsync(IFormFileCollection files)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
			for (int i = 0; i < files.Count; i++)
			{
				var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(files[i].FileName));
				using (var stream = files[i].OpenReadStream())
				{
					await blob.UploadFromStreamAsync(stream);

				}
			}
		}

		/// <summary> 
		/// string GetRandomBlobName(string filename): Generates a unique random file name to be uploaded  
		/// </summary> 
		private string GetRandomBlobName(string filename)
		{
			string ext = Path.GetExtension(filename);
			return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
		}
    }
}