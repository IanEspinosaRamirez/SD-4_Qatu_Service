using Application.Commands.Cart.Create;
using Application.Commands.Cart.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ApiController
{
    private readonly ISender _mediator;

    public CartController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartCommand command)
    {
        var createCartResult = await _mediator.Send(command);

        return createCartResult.Match(_ => StatusCode(201), errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCart([FromRoute] Guid id)
    {
        var deleteCartResult = await _mediator.Send(new DeleteCartCommand(id));

        return deleteCartResult.Match(_ => StatusCode(204), errors => Problem(errors));
    }
}
