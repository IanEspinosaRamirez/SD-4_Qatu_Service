using Application.Commands.Product.Create;
using Application.Commands.Product.Delete;
using Application.Commands.Product.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ApiController
{
    private readonly ISender _mediator;

    public ProductController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult>
    CreateProduct([FromBody] CreateProductCommand command)
    {
        var createProductResult = await _mediator.Send(command);

        return createProductResult.Match(
            _ => StatusCode(201), errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult>
    UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
    {
        var updateProductResult = await _mediator.Send(command);

        return updateProductResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var deleteProductResult = await _mediator.Send(new DeleteProductCommand(id));

        return deleteProductResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }


}
