﻿using System;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace insulin_backend.Services.AzureService
{
    public class AzureBlobConnectionFactory : IAzureBlobConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private CloudBlobClient _blobClient;
        private CloudBlobContainer _blobContainer;
        
        public AzureBlobConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CloudBlobContainer> GetBlobContainer()
        {
            if (_blobContainer != null)
                return _blobContainer;

            var containerName = _configuration.GetValue<string>("ContainerName");
            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentException("Configuration must contain ContainerName");
            }

            var blobClient = GetClient();

            _blobContainer = blobClient.GetContainerReference(containerName);
            if(await _blobContainer.CreateIfNotExistsAsync())
            {
                await _blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
            return _blobContainer;
        }

        private CloudBlobClient GetClient()
        {
            if (_blobClient != null)
                return _blobClient;

            var storageConnectionString = _configuration.GetValue<string>("StorageConnectionString");
            if(string.IsNullOrWhiteSpace(storageConnectionString))
            {
                throw new ArgumentException("Configuration must contain StorageConnectionString");
            }

            if(!CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                throw new Exception("Could not create storage account with StorageConnectionString configuration");
            }

            _blobClient = storageAccount.CreateCloudBlobClient();
            return _blobClient;
        }
    }
}