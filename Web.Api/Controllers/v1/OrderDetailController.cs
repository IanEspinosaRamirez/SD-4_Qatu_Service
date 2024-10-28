using Application.Commands.OrderDetail.Create;
using Application.Commands.OrderDetail.Delete;
using Application.Commands.OrderDetail.GetById;
using Application.Commands.OrderDetail.GetPaged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class OrderDetailController : ApiController
{
    private readonly ISender _mediator;

    public OrderDetailController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail([FromBody] CreateOrderDetailCommand command)
    {
        var createOrderDetailResult = await _mediator.Send(command);

        return createOrderDetailResult.Match(_ => StatusCode(201), errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrderDetail(Guid id)
    {
        var deleteOrderDetailResult = await _mediator.Send(new DeleteOrderDetailCommand(id));

        return deleteOrderDetailResult.Match(_ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderDetail(Guid id)
    {
        var getStoreResult = await _mediator.Send(new GetByIdOrderDetailCommand(id));

        return getStoreResult.Match(store => Ok(store), errors => Problem(errors));
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedOrderDetails(
        [FromQuery] GetPagedOrderDetailsQuery query
    )
    {
        var getOrderDetailsResult = await _mediator.Send(query);

        return getOrderDetailsResult.Match<IActionResult>(
            orderDetails => Ok(orderDetails),
            errors => Problem(errors)
        );
    }
}
