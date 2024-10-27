using Application.Commands.ReviewStores.Create;
using Application.Commands.ReviewStores.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewStoreController : ApiController {
  private readonly ISender _mediator;

  public ReviewStoreController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  public async Task<IActionResult>
  CreateReviewStore([FromBody] CreateReviewStoreCommand command) {
    var createStoreResult = await _mediator.Send(command);

    return createStoreResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteReviewStore(Guid id) {
    var deleteStoreResult =
        await _mediator.Send(new DeleteReviewStoreCommand(id));

    return deleteStoreResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}
