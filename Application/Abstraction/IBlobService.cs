using Application.Data;

namespace Application.Abstraction
{
    public interface IBlobService
    {
        public Task<Result> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToke);

        public Task<Result<BlobDto>> DownloadAsync(string fileName, CancellationToken cancellationToke);
    }
}
