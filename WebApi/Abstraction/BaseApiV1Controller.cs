using Asp.Versioning;
using MediatR;

namespace WebApi.Abstraction
{

    [ApiVersion("1.0")]
    public class BaseApiV1Controller(IMediator mediator) : BaseApiController(mediator)
    {

    }
}
