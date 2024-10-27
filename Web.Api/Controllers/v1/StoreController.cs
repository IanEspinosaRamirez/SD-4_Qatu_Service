using Application.Commands.Store.Create;
using Application.Commands.Store.Delete;
using Application.Commands.Store.GetById;
using Application.Commands.Store.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class StoreController : ApiController {
  private readonly ISender _mediator;

  public StoreController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult>
  CreateStore([FromBody] CreateStoreCommand command) {
    var createStoreResult = await _mediator.Send(command);

    return createStoreResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  [Authorize]
  public async Task<IActionResult> GetStore(Guid id) {
    var getStoreResult = await _mediator.Send(new GetByIdStoreCommand(id));

    return getStoreResult.Match(store => Ok(store), errors => Problem(errors));
  }

  [HttpPut]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult>
  UpdateStore(Guid id, [FromBody] UpdateStoreCommand command) {
    var updateStoreResult = await _mediator.Send(command);

    return updateStoreResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpDelete("{id:guid}")]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult> DeleteStore(Guid id) {
    var deleteStoreResult = await _mediator.Send(new DeleteStoreCommand(id));

    return deleteStoreResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}
