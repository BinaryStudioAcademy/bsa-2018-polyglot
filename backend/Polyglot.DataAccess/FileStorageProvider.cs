using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.DataAccess
{
    public class FileStorageProvider : IFileStorageProvider
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobContainer _cloudBlobContainer;

        public FileStorageProvider(string storageConnectionString,string container = "polyglot")
        {
            _storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            _cloudBlobContainer = _storageAccount
                .CreateCloudBlobClient()
                .GetContainerReference(container);
        }

        public async Task<string> UploadFileAsync(byte[] buffer,FileType type,string extension)
        {
            await SetPublicContainerPermissionsAsync(_cloudBlobContainer);

            string fileName = DirectoryName(type) + "/" + Guid.NewGuid() + "." + extension;

            CloudBlockBlob blob = _cloudBlobContainer.GetBlockBlobReference(fileName);
            await blob.UploadFromByteArrayAsync(buffer, 0, buffer.Length);

            return blob.Uri.ToString();
        }

        public async Task<string> UploadFileAsync(Stream source, FileType type, string extension)
        {
            await SetPublicContainerPermissionsAsync(_cloudBlobContainer);

            string fileName = DirectoryName(type) + "/" + Guid.NewGuid() + "." + extension;

            CloudBlockBlob blob = _cloudBlobContainer.GetBlockBlobReference(fileName);
            await blob.UploadFromStreamAsync(source);

            return blob.Uri.ToString();
        }

        public async Task DeleteFileAsync(string url)
        {
            var path = new Uri(url);
            var client = _storageAccount.CreateCloudBlobClient();
            ICloudBlob blob = null;
            try
            {
                blob = await client.GetBlobReferenceFromServerAsync(path);
            }
            catch (Exception)
            {
                throw new Exception("No such file");
            }
        }

        public async Task<List<string>> GetFilesAsync()
        {
            List<string> result = new List<string>();

            var res = await _cloudBlobContainer.ListBlobsSegmentedAsync(null, null);
            foreach (IListBlobItem item in res.Results)
            {
                if (item is CloudBlobDirectory)
                {
                    var dir = await ((CloudBlobDirectory)item).ListBlobsSegmentedAsync(null);
                    foreach (var nested in dir.Results)
                    {
                        result.Add(nested.Uri.ToString());
                    }
                }
                else
                {
                    result.Add(item.Uri.ToString());
                }
             }

            return result;
        }

        public async Task<List<string>> GetDirectoryFilesAsync(FileType type)
        {
            List<string> result = new List<string>();

            var segments = await _cloudBlobContainer.ListBlobsSegmentedAsync(null, null);
            var dir = segments.Results.FirstOrDefault(x => x is CloudBlobDirectory && x.Uri.ToString().Contains(DirectoryName(type)))
                as CloudBlobDirectory;

            if (dir == null)
                throw new Exception("No such directory");

            var dirItems = await dir.ListBlobsSegmentedAsync(null);

            foreach (IListBlobItem item in dirItems.Results)
            {
                result.Add(item.Uri.ToString());
            }

            return result;
        }

        public enum FileType
        {
            Text,
            Photo,
            ProjectImg,
            Screenshot
        }

        private string DirectoryName(FileType type) => Enum.GetName(typeof(FileType), type).ToLower();
        
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
