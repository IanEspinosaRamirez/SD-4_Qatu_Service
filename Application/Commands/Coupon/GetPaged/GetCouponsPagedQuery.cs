using Application.Commands.Coupon.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.GetPaged;

public record GetCouponsPagedQuery(int PageNumber, int PageSize)
    : IRequest<ErrorOr<List<ResponseGetPagedCouponDto>>>;
