using Application.Files.Download;
using Application.Files.Upload;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApi.Abstraction;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("v{version:apiVersion}/files")]
    public class FilesController : BaseApiV1Controller
    {
        public FilesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadAsync([Required] IFormFile file, CancellationToken cancellationToke)
        {
            var command = new UploadFileCommand(file.OpenReadStream(), file.FileName);

            var result = await _mediator.Send(command, cancellationToke);

            return ActionFromResult(result);
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
