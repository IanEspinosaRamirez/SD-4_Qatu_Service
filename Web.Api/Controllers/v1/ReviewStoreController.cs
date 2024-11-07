using Application.Queries.ReviewStore.GetById;
using Application.Queries.ReviewStore.GetPaged;
using Application.Commands.ReviewStore.Update;
using Application.Commands.ReviewStores.Create;
using Application.Commands.ReviewStores.Delete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Controllers;
using System.Security.Claims;
using Application.Commands.ReviewStores.Create.Dto;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewStoreController : ApiController {
  private readonly ISender _mediator;

  public ReviewStoreController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult>
  CreateReviewStore([FromBody] RequestCreateReviewStoreDto request) {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var command = new CreateReviewStoreCommand(request.rating, request.content,
                                               userIdClaim!, request.storeId);

    var createStoreResult = await _mediator.Send(command);

    return createStoreResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpDelete("{id:guid}")]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult> DeleteReviewStore(Guid id) {
    var deleteStoreResult =
        await _mediator.Send(new DeleteReviewStoreCommand(id));

    return deleteStoreResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  [Authorize]
  public async Task<IActionResult> GetReviewStore(Guid id) {
    var getStoreResult = await _mediator.Send(new GetByIdReviewStoreQuery(id));

    return getStoreResult.Match(store => Ok(store), errors => Problem(errors));
  }

  [HttpPut]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult>
  UpdateReviewStore([FromBody] UpdateReviewStoreCommand command) {

    var updateUserResult = await _mediator.Send(command);

    return updateUserResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult>
  GetReviewStoresPaged(int pageNumber = 1, int pageSize = 10,
                       string? filterField = null, string? filterValue = null,
                       string? orderByField = null, bool ascending = true) {

    var getPagedResult = await _mediator.Send(
        new GetReviewStoresPagedQuery(pageNumber, pageSize, filterField,
                                      filterValue, orderByField, ascending));

    return getPagedResult.Match(reviewStores => Ok(reviewStores),
                                errors => Problem(errors));
  }
}
