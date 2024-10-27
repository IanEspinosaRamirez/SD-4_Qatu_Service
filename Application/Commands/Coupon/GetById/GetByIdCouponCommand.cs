using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.GetById;

public record GetByIdCouponCommand(Guid Id) : IRequest<ErrorOr<DTOs.ResponseGetCouponByIdDto>>;
