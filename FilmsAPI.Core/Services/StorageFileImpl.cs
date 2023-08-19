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
            _connectionString = configuration.GetConnectionString("AzureStorage");
            _env = env;
            _httpContext = httpContext;
            _logger = logger;
        }

        public async Task RemoveFile(string route, string container)
        {
            try
            {
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

        public Task RemoveFileLocal(string route, string container)
        {
            if (route != null)
            {
                var filename = Path.GetFileName(route);
                string fileDirectory = Path.Combine(_env.WebRootPath, container, filename);
                if (File.Exists(fileDirectory))
                {
                    File.Delete(fileDirectory);
                }
            }
            return Task.FromResult(0);
        }

        public async Task<string> SaveFileLocal(byte[] content, string extension, string container, string contentType)
        {
            var filename = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_env.WebRootPath, container);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string route = Path.Combine(folder, filename);
            await File.WriteAllBytesAsync(route, content);
            var currentUrl = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}";
            var urlBD = Path.Combine(currentUrl, container, filename).Replace("\\", "/");
            return urlBD;
        }

        public async Task<string> UpdateFileLocal(byte[] content, string extension, string container, string route, string contentType)
        {
            await RemoveFile(route, container);
            return await SaveFile(content, extension, container, contentType);
        }
    }
}