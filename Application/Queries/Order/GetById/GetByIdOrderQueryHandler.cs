using Application.Queries.Order.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Order.GetById;

internal sealed class GetByIdOrderQueryHandler
    : IRequestHandler<GetByIdOrderQuery, ErrorOr<ResponseGetOrderByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdOrderQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetOrderByIdDto>>
    Handle(GetByIdOrderQuery query, CancellationToken cancellationToken)
    {
        var order =
            await _unitOfWork.OrderRepository.GetById(new CustomerId(query.Id));

        if (order is null)
        {
            return Error.Failure("Order.NotFound", "Order not found.");
        }

        var orderDto = new ResponseGetOrderByIdDto
        {
            TotalPrice = order.TotalPrice,
            ShippingMethod = order.ShippingMethod,
            PaymentMethod = order.PaymentMethod,
            UserId = order.UserId,
        };

        return orderDto;
    }
}
