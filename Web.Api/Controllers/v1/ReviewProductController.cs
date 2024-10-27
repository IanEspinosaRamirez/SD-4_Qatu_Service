using Application.Commands.ReviewProduct.Create;
using Application.Commands.ReviewProduct.Delete;
using Application.Commands.ReviewProduct.GetById;
using Application.Commands.ReviewProduct.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]

public class ReviewProductController : ApiController {
  private readonly ISender _mediator;

  public ReviewProductController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  public async Task<IActionResult>
  CreateReviewProduct([FromBody] CreateReviewProductCommand command) {
    var createStoreResult = await _mediator.Send(command);

    return createStoreResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteReviewProduct(Guid id) {
    var deleteStoreResult =
        await _mediator.Send(new DeleteReviewProductCommand(id));

    return deleteStoreResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetReviewProduct(Guid id) {
    var getStoreResult =
        await _mediator.Send(new GetByIdReviewProductCommand(id));

    return getStoreResult.Match(store => Ok(store), errors => Problem(errors));
  }

  [HttpPut]
  public async Task<IActionResult>
  UpdateReviewProduct([FromBody] UpdateReviewProductCommand command) {

    var updateUserResult = await _mediator.Send(command);

    return updateUserResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}