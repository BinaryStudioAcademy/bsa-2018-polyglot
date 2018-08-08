using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IFileStorageProvider
    {
        Task<string> UploadFileAsync(Stream sorce,FileStorageProvider.FileType type,string extension);
        Task<string> UploadFileAsync(byte[] buffer, FileStorageProvider.FileType type, string extension);
        Task DeleteFileAsync(string url);
        Task<List<string>> GetFilesAsync();
        Task<List<string>> GetDirectoryFilesAsync(FileStorageProvider.FileType type);
    }

}
