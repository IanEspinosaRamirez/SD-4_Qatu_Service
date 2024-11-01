using System.Linq.Expressions;
using Application.Commands.OrderDetail.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.OrderDetail.GetPaged;

internal sealed class GetPagedOrderDetailsQueryHandler
    : IRequestHandler<GetPagedOrderDetailsQuery,
                      ErrorOr<List<ResponseGetPagedOrderDetailDto>>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetPagedOrderDetailsQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<List<ResponseGetPagedOrderDetailDto>>>
  Handle(GetPagedOrderDetailsQuery query, CancellationToken cancellationToken) {
    var orderByExpression =
        GetOrderByExpression<Domain.Entities.OrderDetails.OrderDetail>(
            query.OrderByField);

    var orderDetails = await _unitOfWork.OrderDetailRepository.GetPaged(
        query.PageNumber, query.PageSize, query.FilterField, query.FilterValue,
        orderByExpression, query.Ascending);

    if (!orderDetails.Any()) {
      return Error.Failure("OrderDetail.NoRecords", "No order details found.");
    }

    var orderDetailDtos =
        orderDetails
            .Select(orderDetail => new ResponseGetPagedOrderDetailDto {
              Quantity = orderDetail.Quantity,
              UnitPrice = orderDetail.UnitPrice,
            })
            .ToList();

    return orderDetailDtos;
  }

  private Expression<Func<T, object>>? GetOrderByExpression<T>(
      string? orderByField) {
    if (string.IsNullOrEmpty(orderByField)) {
      return null;
    }

    var parameter = Expression.Parameter(typeof(T), "x");
    var property = Expression.PropertyOrField(parameter, orderByField);
    var conversion = Expression.Convert(property, typeof(object));
    return Expression.Lambda<Func<T, object>>(conversion, parameter);
  }
}
