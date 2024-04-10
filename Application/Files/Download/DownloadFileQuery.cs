using Application.Abstraction;
using Application.Data;

namespace Application.Files.Download
{
    public record DownloadFileQuery(string FileName) : IQuery<BlobDto>;
}
