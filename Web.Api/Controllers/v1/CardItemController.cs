using Application.Commands.CartItem.Create;
using Application.Commands.CartItem.Delete;
using Application.Commands.CartItem.GetById;
using Application.Commands.CartItem.Update;
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

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteCartItem(Guid id) {
    var deleteCategoryResult =
        await _mediator.Send(new DeleteCartItemCommand(id));

    return deleteCategoryResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetByIdCartItem([FromRoute] Guid id) {
    var getCartResult = await _mediator.Send(new GetByIdCartItemCommand(id));

    return getCartResult.Match<IActionResult>(cart => Ok(cart),
                                              errors => Problem(errors));
  }

  [HttpPut]
  public async Task<IActionResult>
  UpdateCartItem([FromBody] UpdateCartItemCommand command) {
    var updateUserResult = await _mediator.Send(command);

    return updateUserResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}
