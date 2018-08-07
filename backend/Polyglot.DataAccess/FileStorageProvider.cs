using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.DataAccess
{
    class FileStorageProvider : IFileStorageProvider
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly CloudBlobContainer _cloudBlobContainer;

        public FileStorageProvider(string storageConnectionString,string container)
        {
            _storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            _cloudBlobClient = _storageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = _cloudBlobClient.GetContainerReference(container);
        }

        public async Task<string> UploadFileAsync(byte[] buffer)
        {
            await _cloudBlobContainer.CreateAsync();

            await SetPublicContainerPermissionsAsync(_cloudBlobContainer);

            string localFileName = "polyglot_" + Guid.NewGuid() + ".txt";
            CloudBlockBlob blob = _cloudBlobContainer.GetBlockBlobReference(localFileName);
            await blob.UploadFromByteArrayAsync(buffer, 0, buffer.Length);
            return blob.Uri.ToString();
        }

        public async Task<string> UploadFileAsync(Stream source)
        {
            await _cloudBlobContainer.CreateAsync();

            await SetPublicContainerPermissionsAsync(_cloudBlobContainer);

            string localFileName = "polyglot_" + Guid.NewGuid() + ".txt";
            CloudBlockBlob blob = _cloudBlobContainer.GetBlockBlobReference(localFileName);
            await blob.UploadFromStreamAsync(source);
            return blob.Uri.ToString();
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
