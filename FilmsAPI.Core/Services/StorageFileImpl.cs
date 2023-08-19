using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FilmsAPI.Core.Services
{
    public class StorageFileImpl : IStorageFiles
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<StorageFileImpl> _logger;

        public StorageFileImpl(
            IConfiguration configuration,
            IWebHostEnvironment env,
            IHttpContextAccessor httpContext,
            ILogger<StorageFileImpl> logger
        )
        {
            _connectionString = string.IsNullOrEmpty(configuration.GetConnectionString("AzureStorage")) ?
                configuration.GetConnectionString("AzureStorage") : "";
            _env = env;
            _httpContext = httpContext;
            _logger = logger;
        }

        public async Task RemoveFile(string route, string container)
        {
            try
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _logger.LogInformation("The connection string is null !!!, however it will try to remove a file that is in the local repository");
                    await _removeFileLocal(route, container);
                    return;
                }

                if (string.IsNullOrEmpty(route))
                {
                    return;
                }
                var client = new BlobContainerClient(_connectionString, container);
                await client.CreateIfNotExistsAsync();
                var file = Path.GetFileName(route);
                var blob = client.GetBlobClient(file);
                await blob.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Internal Core Upload File Exception: {ex.Message}");
            }
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            try
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _logger.LogInformation("The connection string is null !!!, therefore the image will not be uploaded to Azure Storage, but to a local repository");
                    return await _saveFileLocal(content, extension, container, contentType);
                }

                var client = new BlobContainerClient(_connectionString, container);
                await client.CreateIfNotExistsAsync();
                client.SetAccessPolicy(PublicAccessType.Blob);
                var filename = $"{Guid.NewGuid()}{extension}";
                var blob = client.GetBlobClient(filename);
                var blobUploadOptions = new BlobUploadOptions();
                var blobHttpHeader = new BlobHttpHeaders();
                blobHttpHeader.ContentType = contentType;
                blobUploadOptions.HttpHeaders = blobHttpHeader;
                await blob.UploadAsync(new BinaryData(content), blobUploadOptions);
                return blob.Uri.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Internal Core Upload File Exception: {ex.Message}");
                return ex.Message;
            }
        }

        public async Task<string> UpdateFile(byte[] content, string extension, string container, string route, string contentType)
        {
            await RemoveFile(route, container);
            return await SaveFile(content, extension, container, contentType);
        }

        private Task _removeFileLocal(string route, string container)
        {
            _logger.LogInformation(route, container);
            if (route != null)
            {
                var filename = Path.GetFileName(route);
                _logger.LogInformation(filename);
                string fileDirectory = Path.Combine(_env.WebRootPath, container, filename);
                _logger.LogInformation(fileDirectory);
                if (File.Exists(fileDirectory))
                {
                    File.Delete(fileDirectory);
                }
            }
            return Task.FromResult(0);
        }

        private async Task<string> _saveFileLocal(byte[] content, string extension, string container, string contentType)
        {
            _logger.LogInformation($"Extension del archivo: {extension}");
            var filename = $"{Guid.NewGuid()}{extension}";
            _logger.LogInformation($"Nombre del Archivo: {filename}");
            _logger.LogInformation($"_env.WebRootPath: {_env.WebRootPath}");
            string folder = Path.Combine(_env.WebRootPath, container);
            _logger.LogInformation($"Folder: {folder}");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string route = Path.Combine(folder, filename);
            _logger.LogInformation($"Route: {route}");
            await File.WriteAllBytesAsync(route, content);
            var currentUrl = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}";
            _logger.LogInformation($"CurrentUrl: {currentUrl}");
            var urlBD = Path.Combine(currentUrl, container, filename).Replace("\\", "/");
            _logger.LogInformation($"URlBD: {urlBD}");
            return urlBD;
        }
    }
}