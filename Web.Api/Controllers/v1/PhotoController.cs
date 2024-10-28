using Application.Commands.Photo.Create;
using Application.Queries.Photo.GetPhotoUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class PhotoController : ApiController
{
    private readonly ISender _mediator;

    public PhotoController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult>
    CreatePhoto([FromBody] CreatePhotoCommand command)
    {
        var createPhotoResult = await _mediator.Send(command);

        return createPhotoResult.Match(
            _ => StatusCode(201), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPhoto(Guid id)
    {
        var getPhotoResult = await _mediator.Send(new GetByIdPhotoQuery(id));

        return getPhotoResult.Match(photo => Ok(photo), errors => Problem(errors));
    }
}
