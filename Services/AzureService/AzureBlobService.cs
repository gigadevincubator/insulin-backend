using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using insulin_backend.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;

namespace insulin_backend.Services.AzureService
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;
        private DataContext dbContext;

        public AzureBlobService(IAzureBlobConnectionFactory azureBlobConnectionFactory, DataContext dbContext)
        {
            _azureBlobConnectionFactory = azureBlobConnectionFactory;
            this.dbContext = dbContext;
        }

        private async Task<string> UploadAsync(IFormFileCollection files)
        {
            string blobUrl = null;
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
            foreach (var t in files)
            {
                var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(t.FileName));
                blobUrl = blob.Uri.AbsoluteUri;
                await using var stream = t.OpenReadStream();
                await blob.UploadFromStreamAsync(stream);
            }
            return blobUrl;
        }

        public async Task UploadStepVideoAsync(IFormFileCollection files, int stepVideoId)
        {
            var  blobUrl=await UploadAsync(files);
            if (string.IsNullOrEmpty(blobUrl))
            {
                throw new Exception("Something went wrong");
            }
            AddStepVideoUrl(blobUrl, stepVideoId);
        }

        public async Task UploadStepLanguageAudioAsync(IFormFileCollection files, int stepLanguageId)
        {
            var  blobUrl=await UploadAsync(files);
            if (string.IsNullOrEmpty(blobUrl))
            {
                throw new Exception("Something went wrong");
            }
            AddStepLanguageAudioUrl(blobUrl, stepLanguageId);
        }

        public async Task UploadTutorialThumbnailAsync(IFormFileCollection files, int tutorialThumbnailId)
        {
            var  blobUrl=await UploadAsync(files);
            if (string.IsNullOrEmpty(blobUrl))
            {
                throw new Exception("Something went wrong");
            }
            AddTutorialThumbnailUrl(blobUrl,tutorialThumbnailId);
        }

        private void AddStepLanguageAudioUrl(string blobUrl, int stepLanguageId)
        {
            var entity = dbContext.StepLanguage.FirstOrDefault(item => item.Id == stepLanguageId);
            if (entity == null) return;
            entity.AudioUrl = blobUrl;
            // Update entity BLOB Url in DbSet
            dbContext.StepLanguage.Update(entity);

            // Save changes in database
            dbContext.SaveChanges();
        }
        
        private void AddStepVideoUrl(string blobUrl, int stepVideoId)
        {
            var entity = dbContext.Steps.FirstOrDefault(item => item.Id == stepVideoId);
            if (entity == null) return;
            entity.VideoUrl = blobUrl;
            // Update entity BLOB Url in DbSet
            dbContext.Steps.Update(entity);

            // Save changes in database
            dbContext.SaveChanges();
        }
        
        private void AddTutorialThumbnailUrl(string blobUrl, int tutorialId)
        {
            var entity = dbContext.Tutorials.FirstOrDefault(item => item.Id == tutorialId);
            if (entity != null)
            {
                entity.ThumbnailUrl = blobUrl;
                // Update entity BLOB Url in DbSet
                dbContext.Tutorials.Update(entity);

                // Save changes in database
                dbContext.SaveChanges();
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