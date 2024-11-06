using Application.Commands.Product.Create;
using Application.Commands.Product.Delete;
using Application.Queries.Product.GetById;
using Application.Queries.Product.GetPaged;
using Application.Commands.Product.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ApiController {
  private readonly ISender _mediator;

  public ProductController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult>
  CreateProduct([FromBody] CreateProductCommand command) {
    var createProductResult = await _mediator.Send(command);

    return createProductResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  [Authorize]
  public async Task<IActionResult> GetProduct(Guid id) {
    var getProductResult = await _mediator.Send(new GetByIdProductQuery(id));

    return getProductResult.Match(product => Ok(product),
                                  errors => Problem(errors));
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult>
  GetProductsPaged(int pageNumber = 1, int pageSize = 10,
                   string? filterField = null, string? filterValue = null,
                   string? orderByField = null, bool ascending = true) {

    var getPagedResult = await _mediator.Send(
        new GetProductsPagedQuery(pageNumber, pageSize, filterField,
                                  filterValue, orderByField, ascending));

    return getPagedResult.Match(products => Ok(products),
                                errors => Problem(errors));
  }

  [HttpPut]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult>
  UpdateProduct(Guid id, [FromBody] UpdateProductCommand command) {
    var updateProductResult = await _mediator.Send(command);

    return updateProductResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpDelete("{id:guid}")]
  [Authorize(Roles = "Administrator, Seller")]
  public async Task<IActionResult> DeleteProduct(Guid id) {
    var deleteProductResult =
        await _mediator.Send(new DeleteProductCommand(id));

    return deleteProductResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}
