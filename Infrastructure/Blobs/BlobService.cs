using Application.Abstraction;
using Application.Data;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

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
            _containerClient.CreateIfNotExists(PublicAccessType.Blob);
        }

        public async Task<Result> UploadAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, true, cancellationToken);
            return Result.CreateSuccessful();
        }

        public async Task<Result<BlobDto>>
            DownloadAsync(string fileName, CancellationToken cancellationToke)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream, cancellationToke);
            memoryStream.Position = 0;
            var contentType = blobClient.GetProperties().Value.ContentType;
            return Result<BlobDto>
                .CreateSuccessful(new(memoryStream, contentType));
        }

        public async Task<bool> ExistsAsync(string fileName, CancellationToken cancellationToke)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            return await blobClient.ExistsAsync(cancellationToke);
        }
    }
}
