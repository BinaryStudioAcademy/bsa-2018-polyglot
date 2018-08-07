using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IFileStorageProvider
    {
        Task<string> UploadFileAsync(string path);
        Task DeleteFileAsync(string url);
        Task<List<string>> GetFilesAsync();
    }
}
