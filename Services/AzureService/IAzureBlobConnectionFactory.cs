using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;

namespace insulin_backend.Services.AzureService
{
    public interface IAzureBlobConnectionFactory
    {
        Task<CloudBlobContainer> GetBlobContainer();
    }
}