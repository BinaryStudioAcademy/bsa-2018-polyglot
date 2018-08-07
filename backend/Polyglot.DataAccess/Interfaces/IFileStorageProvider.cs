using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IFileStorageProvider
    {
        Task<string> UploadFileAsync(Stream sorce);
        Task<string> UploadFileAsync(byte[] buffer);
        Task DeleteFileAsync(string url);
        Task<List<string>> GetFilesAsync();
    }
}
