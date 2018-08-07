using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
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

        public Task<string> UploadFileAsync(string path)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteFileAsync(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<string>> GetFilesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
