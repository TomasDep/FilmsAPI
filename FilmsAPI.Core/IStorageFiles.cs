namespace FilmsAPI.Core
{
    public interface IStorageFiles
    {
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
        Task<string> UpdateFile(byte[] content, string extension, string container, string route, string contentType);
        Task RemoveFile(string route, string container);
        Task<string> SaveFileLocal(byte[] content, string extension, string container, string contentType);
        Task<string> UpdateFileLocal(byte[] content, string extension, string container, string route, string contentType);
        Task RemoveFileLocal(string route, string container);
    }
}