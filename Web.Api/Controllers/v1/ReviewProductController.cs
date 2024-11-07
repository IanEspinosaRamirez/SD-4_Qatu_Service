using Application.Commands.ReviewProduct.Create;
using Application.Commands.ReviewProduct.Delete;
using Application.Queries.ReviewProduct.GetById;
using Application.Queries.ReviewProduct.GetPaged;
using Application.Commands.ReviewProduct.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Controllers;
using Application.Commands.ReviewProduct.Create.Dto;
using System.Security.Claims;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewProductController : ApiController
{
    private readonly ISender _mediator;

    public ReviewProductController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult>
    CreateReviewProduct([FromBody] RequestCreateReviewProductDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new CreateReviewProductCommand(
            request.rating, request.content, userIdClaim!, request.productId);

        var createStoreResult = await _mediator.Send(command);

        return createStoreResult.Match(
            _ => StatusCode(201), errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteReviewProduct(Guid id)
    {
        var deleteStoreResult =
            await _mediator.Send(new DeleteReviewProductCommand(id));

        return deleteStoreResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetReviewProduct(Guid id)
    {
        var getStoreResult =
            await _mediator.Send(new GetByIdReviewProductQuery(id));

        return getStoreResult.Match(store => Ok(store), errors => Problem(errors));
    }

    [HttpPut]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult>
    UpdateReviewProduct([FromBody] UpdateReviewProductCommand command)
    {

        var updateUserResult = await _mediator.Send(command);

        return updateUserResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult>
    GetReviewProductsPaged(int pageNumber = 1, int pageSize = 10,
                           string? filterField = null, string? filterValue = null,
                           string? orderByField = null, bool ascending = true)
    {

        var getPagedResult = await _mediator.Send(
            new GetReviewProductsPagedQuery(pageNumber, pageSize, filterField,
                                            filterValue, orderByField, ascending));

        return getPagedResult.Match(reviewProducts => Ok(reviewProducts),
                                    errors => Problem(errors));
    }
}
