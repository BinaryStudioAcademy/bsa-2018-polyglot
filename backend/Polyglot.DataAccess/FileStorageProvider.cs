using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.DataAccess
{
    class FileStorageProvider : IFileStorageProvider
    {
        private CloudStorageAccount _storageAccount;

        public FileStorageProvider(string storageConnectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(storageConnectionString);
        }

        public async Task<string> UploadFileAsync(string path)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteFileAsync(string url)
        {
            var path = new Uri(url);
            var client = _storageAccount.CreateCloudBlobClient();
            var blob = await client.GetBlobReferenceFromServerAsync(path);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetFilesAsync()
        {
            List<string> result = new List<string>();
            CloudBlobClient cloudBlobClient = _storageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("polyglot");
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var res = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                blobContinuationToken = res.ContinuationToken;
                foreach (IListBlobItem item in res.Results)
                {
                    result.Add(item.Uri.ToString());
                }
            }
            while (blobContinuationToken != null);

            return result;
        }

        public enum FileType
        {
            Text,
            Photo,
            ProjectImg,
            Screenshot
        }

        private async Task SetPublicContainerPermissionsAsync(CloudBlobContainer container)
        {
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await container.SetPermissionsAsync(permissions);
        }
    }
}
