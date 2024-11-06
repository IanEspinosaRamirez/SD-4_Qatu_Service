using Application.Queries.Coupon.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.Coupon.GetPaged;

public record
GetCouponsPagedQuery(int PageNumber, int PageSize, string? FilterField = null,
                     string? FilterValue = null, string? OrderByField = null,
                     bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedCouponDto>>>;
