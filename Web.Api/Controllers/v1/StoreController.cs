using Application.Commands.Store.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]

public class StoreController : ApiController
{
    private readonly ISender _mediator;

    public StoreController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult>
    CreateStore([FromBody] CreateStoreCommand command)
    {
        var createStoreResult = await _mediator.Send(command);

        return createStoreResult.Match(
            _ => StatusCode(201), errors => Problem(errors));
    }
}
