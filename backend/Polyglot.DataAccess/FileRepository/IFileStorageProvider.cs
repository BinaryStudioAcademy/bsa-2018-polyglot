using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Polyglot.DataAccess.FileRepository;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IFileStorageProvider
    {
        Task<string> UploadFileAsync(Stream sorce, FileType type, string extension);
        Task<string> UploadFileAsync(byte[] buffer, FileType type, string extension);
        Task DeleteFileAsync(string url);
        Task<List<string>> GetFilesAsync();
        Task<List<string>> GetDirectoryFilesAsync(FileType type);
    }

}
