using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.Delete;

public record DeleteCouponCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
