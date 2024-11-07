using Application.Commands.Category.Create;
using Application.Commands.Category.Delete;
using Application.Commands.Category.Update;
using Application.Queries.Category.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.Categories.GetAll;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ApiController
{
    private readonly ISender _mediator;

    public CategoryController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult>
    CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var createCategoryResult = await _mediator.Send(command);

        return createCategoryResult.Match(
            _ => StatusCode(201), errors => Problem(errors));
    }

    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult>
    UpdateCategory([FromBody] UpdateCategoryCommand command)
    {
        var updateCategoryResult = await _mediator.Send(command);

        return updateCategoryResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var getCategoryResult = await _mediator.Send(new GetByIdCategoryQuery(id));

        return getCategoryResult.Match(categoryDto => Ok(categoryDto),
                                       errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var deleteCategoryResult =
            await _mediator.Send(new DeleteCategoryCommand(id));

        return deleteCategoryResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCategories()
    {
        var getAllCategoriesResult =
            await _mediator.Send(new GetAllCategoriesQuery());

        return getAllCategoriesResult.Match(categories => Ok(categories),
                                            errors => Problem(errors));
    }
}
