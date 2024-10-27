using FluentValidation;

namespace Application.Commands.Coupon.Update;

internal sealed class UpdateCouponCommandValidation : AbstractValidator<UpdateCouponCommand>
{
    public UpdateCouponCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Coupon ID is required.");

        RuleFor(x => x.DiscountPercentage)
            .InclusiveBetween(0, 100)
            .WithMessage("Discount percentage must be between 0 and 100.");

        RuleFor(x => x.ExpirationDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Expiration date must be in the future.");

        RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive status is required.");

        RuleFor(x => x.TypeCoupon).NotEmpty().WithMessage("TypeCoupon is required.");

        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");

        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(x => x.StoreId).NotEmpty().WithMessage("StoreId is required.");
    }
}
