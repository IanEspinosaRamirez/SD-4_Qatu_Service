using Application.Commands.Cart.Create;
using Application.Queries.Cart.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[Authorize(Roles = "Client")]
[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ApiController {
  private readonly ISender _mediator;

  public CartController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  [Authorize]
  public async Task<IActionResult>
  CreateCart([FromBody] CreateCartCommand command) {
    var createCartResult = await _mediator.Send(command);

    return createCartResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  [Authorize]
  public async Task<IActionResult> GetByIdCart([FromRoute] Guid id) {
    var getCartResult = await _mediator.Send(new GetByIdCartQuery(id));

    return getCartResult.Match<IActionResult>(cart => Ok(cart),
                                              errors => Problem(errors));
  }
}
