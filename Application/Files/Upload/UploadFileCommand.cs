using Application.Abstraction;

namespace Application.Files.Upload
{
    public record UploadFileCommand(Stream FileStream, string FileName) : ICommand;
}
