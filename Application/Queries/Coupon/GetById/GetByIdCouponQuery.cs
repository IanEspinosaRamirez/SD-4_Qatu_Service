using ErrorOr;
using MediatR;

namespace Application.Queries.Coupon.GetById;

public record GetByIdCouponQuery(Guid Id)
    : IRequest<ErrorOr<DTOs.ResponseGetCouponByIdDto>>;
