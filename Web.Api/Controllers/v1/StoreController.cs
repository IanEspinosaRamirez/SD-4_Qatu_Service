using Application.Commands.Store.Create;
using Application.Commands.Store.Delete;
using Application.Queries.Store.GetById;
using Application.Queries.Store.GetPaged;
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
    var getStoreResult = await _mediator.Send(new GetByIdStoreQuery(id));

    return getStoreResult.Match(store => Ok(store), errors => Problem(errors));
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult>
  GetStoresPaged(int pageNumber = 1, int pageSize = 10,
                 string? filterField = null, string? filterValue = null,
                 string? orderByField = null, bool ascending = true) {

    var getPagedResult = await _mediator.Send(
        new GetStoresPagedQuery(pageNumber, pageSize, filterField, filterValue,
                                orderByField, ascending));

    return getPagedResult.Match(stores => Ok(stores),
                                errors => Problem(errors));
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
