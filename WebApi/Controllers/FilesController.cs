using Application.Data;
using Application.Files.Download;
using Application.Files.Upload;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("files/v{version:apiVersion}")]
    public class FilesController : Controller
    {
        private readonly IMediator _mediator;
        public FilesController(IMediator mediator) => _mediator = mediator;

        [HttpPost("upload")]
        public async Task<Result> UploadAsync([Required] IFormFile file, CancellationToken cancellationToke)
        {
            var command = new UploadFileCommand(file.OpenReadStream(), file.FileName);

            var result = await _mediator.Send(command, cancellationToke);

            return result;
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadAsync([Required] string fileName, CancellationToken cancellationToke)
        {
            var query = new DownloadFileQuery(fileName);

            var result = await _mediator.Send(query, cancellationToke);

            if (result.Success)
                return File(result.Value.MemoryStream, result.Value.ContentType, fileName);
            else
                return NotFound(result.ErrorMessage);
        }
    }
}
