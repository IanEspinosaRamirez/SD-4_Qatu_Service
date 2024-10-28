using Application.Commands.Order.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.GetPaged;

internal sealed class GetPagedOrdersQueryHandler
    : IRequestHandler<GetPagedOrdersQuery, ErrorOr<List<ResponseGetPagedOrderDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedOrdersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedOrderDto>>> Handle(
        GetPagedOrdersQuery request,
        CancellationToken cancellationToken
    )
    {
        var orders = await _unitOfWork.OrderRepository.GetPaged(
            request.PageNumber,
            request.PageSize
        );

        if (!orders.Any())
        {
            return Error.Failure("Order.NoRecords", "No orders found.");
        }

        var orderDtos = orders
            .Select(order => new ResponseGetPagedOrderDto
            {
                TotalPrice = order.TotalPrice,
                ShippingMethod = order.ShippingMethod,
                PaymentMethod = order.PaymentMethod,
                UserId = order.UserId,
            })
            .ToList();

        return orderDtos;
    }
}
