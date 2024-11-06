using System.Linq.Expressions;
using Application.Queries.Coupon.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Coupon.GetPaged;

internal sealed class GetCouponsPagedQueryHandler
    : IRequestHandler<GetCouponsPagedQuery,
                      ErrorOr<List<ResponseGetPagedCouponDto>>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetCouponsPagedQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<List<ResponseGetPagedCouponDto>>>
  Handle(GetCouponsPagedQuery query, CancellationToken cancellationToken) {
    var orderByExpression =
        GetOrderByExpression<Domain.Entities.Coupons.Coupon>(
            query.OrderByField);

    var coupons = await _unitOfWork.CouponRepository.GetPaged(
        query.PageNumber, query.PageSize, query.FilterField, query.FilterValue,
        orderByExpression, query.Ascending);

    if (!coupons.Any()) {
      return Error.Failure("Coupon.NoRecords", "No coupons found.");
    }

    var couponDtos = coupons
                         .Select(coupon => new ResponseGetPagedCouponDto {
                           DiscountPercentage = coupon.DiscountPercentage,
                           ExpirationDate = coupon.ExpirationDate,
                           IsActive = coupon.IsActive,
                           TypeCoupon = coupon.TypeCoupon,
                           ProductId = coupon.ProductId,
                           CategoryId = coupon.CategoryId,
                           StoreId = coupon.StoreId,
                         })
                         .ToList();

    return couponDtos;
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
