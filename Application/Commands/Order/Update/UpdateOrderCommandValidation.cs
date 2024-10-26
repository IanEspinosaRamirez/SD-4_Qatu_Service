using FluentValidation;

namespace Application.Commands.Order.Update;

public sealed class UpdateOrderCommandValidation : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidation()
    {
        RuleFor(x => x.TotalPrice).GreaterThan(0);
        RuleFor(x => x.ShippingMethod).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
