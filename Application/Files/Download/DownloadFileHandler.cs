using Application.Abstraction;
using Application.Data;
using MediatR;

namespace Application.Files.Download
{
    public class DownloadFileHandler : IQueryHandler<DownloadFileQuery, BlobDto>
    {
        private readonly IBlobService _blobService;
        public DownloadFileHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public Task<Result<BlobDto>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
            => _blobService.DownloadAsync(request.FileName, cancellationToken);
    }
}
