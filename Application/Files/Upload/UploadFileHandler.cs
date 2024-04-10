using Application.Abstraction;
using Application.Data;

namespace Application.Files.Upload
{
    internal class UploadFileHandler : ICommandHandler<UploadFileCommand>
    {
        private readonly IBlobService _blobService;
        public UploadFileHandler(IBlobService blobService) 
        {
            _blobService = blobService;
        }

        public Task<Result> Handle(UploadFileCommand request, CancellationToken cancellationToken)
            => _blobService.UploadAsync(request.FileStream, request.FileName, cancellationToken);
    }
}
