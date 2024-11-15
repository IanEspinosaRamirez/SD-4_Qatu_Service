using Application.Commands.Photo.Create;
using Application.Commands.Photo.Delete;
using Application.Queries.Photo.GetPagedPhotos;
using Application.Queries.Photo.GetPhotosByProductId;
using Application.Queries.Photo.GetPhotosByStoreId;
using Application.Queries.Photo.GetPhotoUrl;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class PhotoController : ApiController
{
    private readonly ISender _mediator;

    public PhotoController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Seller")]
    public async Task<IActionResult>
    CreatePhoto([FromBody] CreatePhotoCommand command)
    {
        var createPhotoResult = await _mediator.Send(command);

        return createPhotoResult.Match(
            _ => StatusCode(201), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPhoto(Guid id)
    {
        var getPhotoResult = await _mediator.Send(new GetByIdPhotoQuery(id));

        return getPhotoResult.Match(photo => Ok(photo), errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator, Seller")]
    public async Task<IActionResult> DeletePhoto(Guid id)
    {
        var command = new DeletePhotoCommand(id);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => NoContent(), errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult>
    GetPhotosPaged(int pageNumber = 1, int pageSize = 10,
                   string? filterField = null, string? filterValue = null,
                   string? orderByField = null, bool ascending = true)
    {

        var getPagedResult = await _mediator.Send(
            new GetPhotosPagedQuery(pageNumber, pageSize, filterField, filterValue,
                                    orderByField, ascending));

        return getPagedResult.Match(photos => Ok(photos),
                                    errors => Problem(errors));
    }

    [HttpGet("by-product/{productId}")]
    public async Task<IActionResult> GetPhotosByProductId(string productId)
    {
        var query = new GetPhotosByProductIdQuery(productId);
        var result = await _mediator.Send(query);

        return result.Match(photos => Ok(photos), errors => Problem(errors));
    }

    [HttpGet("by-store/{storeId}")]
    public async Task<IActionResult> GetPhotosByStoreId(string storeId)
    {
        var query = new GetPhotosByStoreIdQuery(storeId);
        var result = await _mediator.Send(query);

        return result.Match(photos => Ok(photos), errors => Problem(errors));
    }
}
