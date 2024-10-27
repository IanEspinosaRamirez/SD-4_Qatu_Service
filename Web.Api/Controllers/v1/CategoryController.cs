using Application.Commands.Category.Create;
using Application.Commands.Category.Delete;
using Application.Commands.Category.Update;
using Application.Commands.Category.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ApiController {
  private readonly ISender _mediator;

  public CategoryController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  public async Task<IActionResult>
  CreateCategory([FromBody] CreateCategoryCommand command) {
    var createCategoryResult = await _mediator.Send(command);

    return createCategoryResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpPut]
  public async Task<IActionResult>
  UpdateCategory([FromBody] UpdateCategoryCommand command) {
    var updateCategoryResult = await _mediator.Send(command);

    return updateCategoryResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetCategoryById(Guid id) {
    var getCategoryResult =
        await _mediator.Send(new GetByIdCategoryCommand(id));

    return getCategoryResult.Match(categoryDto => Ok(categoryDto),
                                   errors => Problem(errors));
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteCategory(Guid id) {
    var deleteCategoryResult =
        await _mediator.Send(new DeleteCategoryCommand(id));

    return deleteCategoryResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}
