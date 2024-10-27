using Application.Commands.CartItem.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CardItemController : ApiController {
  private readonly ISender _mediator;

  public CardItemController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  public async Task<IActionResult>
  CreateCartItem([FromBody] CreateCartItemCommand command) {
    var createCartResult = await _mediator.Send(command);

    return createCartResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }
}
