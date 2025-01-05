using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;

namespace SecureFileUpload.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;
        private readonly string _accountKey;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["BlobStorage:ConnectionString"];
            _accountKey = configuration["BlobStorage:AccountKey"];
            _containerName = configuration["BlobStorage:ContainerName"];

            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(_accountKey) || string.IsNullOrEmpty(_containerName))
                throw new ArgumentException("Missing Blob Storage configuration values.");

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, int expiryHours)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be null or empty.");

            try
            {
                // Get or create container
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await containerClient.CreateIfNotExistsAsync();

                // Generate unique filename
                string uniqueFileName = $"{Guid.NewGuid()}-{file.FileName}";
                var blobClient = containerClient.GetBlobClient(uniqueFileName);

                // Upload file
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }

                // Generate SAS token
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = _containerName,
                    BlobName = uniqueFileName,
                    Resource = "b",
                    StartsOn = DateTimeOffset.UtcNow,
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(expiryHours)
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                var sasToken = sasBuilder.ToSasQueryParameters(
                    new Azure.Storage.StorageSharedKeyCredential(
                        _blobServiceClient.AccountName,
                        _accountKey)).ToString();

                return $"{blobClient.Uri}?{sasToken}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading file: {ex.Message}");
            }
        }
    }
}
