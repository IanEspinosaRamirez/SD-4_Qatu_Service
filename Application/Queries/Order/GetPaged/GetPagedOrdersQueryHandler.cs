using System.Linq.Expressions;
using Application.Queries.Order.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Order.GetPaged;

internal sealed class GetPagedOrdersQueryHandler
    : IRequestHandler<GetPagedOrdersQuery,
                      ErrorOr<List<ResponseGetPagedOrderDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedOrdersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedOrderDto>>>
    Handle(GetPagedOrdersQuery query, CancellationToken cancellationToken)
    {
        var orderByExpression =
            GetOrderByExpression<Domain.Entities.Orders.Order>(query.OrderByField);

        var orders = await _unitOfWork.OrderRepository.GetPaged(
            query.PageNumber, query.PageSize, query.FilterField, query.FilterValue,
            orderByExpression, query.Ascending);

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

    private Expression<Func<T, object>>? GetOrderByExpression<T>(
        string? orderByField)
    {
        if (string.IsNullOrEmpty(orderByField))
        {
            return null;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, orderByField);
        var conversion = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(conversion, parameter);
    }
}
