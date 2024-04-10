using Application.Abstraction;
using Application.Data;
using Azure.Storage.Blobs;

namespace Infrastructure.Blobs
{
    internal class BlobService : IBlobService
    {
        public const string PicturesContainer = "pictures-ddd-container";

        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(PicturesContainer);
            _containerClient.CreateIfNotExists();
        }

        public async Task<Result> UploadAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(fileStream, true, cancellationToken);
                return Result.CreateSuccessful();
            }
            catch (Exception ex)
            {
                return Result.CreateFailed("An error occurred while uploading the file: " + ex.Message);
            }
        }

        public async Task<Result<BlobDto>> 
            DownloadAsync(string fileName, CancellationToken cancellationToke)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(fileName);
                var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream, cancellationToke);
                memoryStream.Position = 0;
                var contentType = blobClient.GetProperties().Value.ContentType;
                return Result<BlobDto>
                    .CreateSuccessful(new (memoryStream, contentType));
            }
            catch (Exception ex)
            {
                return Result<BlobDto>
                    .CreateFailed("An error occurred while downloading the file: " + ex.Message);
            }
        }
    }
}
