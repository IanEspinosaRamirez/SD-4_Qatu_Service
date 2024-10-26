using FluentValidation;

namespace Application.Commands.Order.Create;

internal sealed class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidation()
    {
        RuleFor(x => x.TotalPrice).GreaterThan(0);
        RuleFor(x => x.ShippingMethod).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
