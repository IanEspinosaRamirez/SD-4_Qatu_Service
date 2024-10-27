using FluentValidation;

namespace Application.Commands.Coupon.Delete;

internal sealed class DeleteCouponCommandValidation : AbstractValidator<DeleteCouponCommand>
{
    public DeleteCouponCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
