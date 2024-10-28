using Application.Commands.Order.Create;
using Application.Commands.Order.Delete;
using Application.Commands.Order.GetById;
using Application.Commands.Order.GetPaged;
using Application.Commands.Order.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ApiController
{
    private readonly ISender _mediator;

    public OrderController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var createOrderResult = await _mediator.Send(command);

        return createOrderResult.Match(_ => StatusCode(201), errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderCommand command)
    {
        var updateOrderResult = await _mediator.Send(command);

        return updateOrderResult.Match(_ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdOrder(Guid id)
    {
        var getOrderResult = await _mediator.Send(new GetByIdOrderCommand(id));

        return getOrderResult.Match<IActionResult>(order => Ok(order), errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var deleteOrderResult = await _mediator.Send(new DeleteOrderCommand(id));

        return deleteOrderResult.Match(_ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedOrder([FromQuery] GetPagedOrdersQuery query)
    {
        var getOrdersResult = await _mediator.Send(query);

        return getOrdersResult.Match<IActionResult>(
            orders => Ok(orders),
            errors => Problem(errors)
        );
    }
}
