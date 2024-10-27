using FluentValidation;

namespace Application.Commands.OrderDetail.Create;

internal sealed class CreateOrderDetailCommandValidation
    : AbstractValidator<CreateOrderDetailCommand>
{
    public CreateOrderDetailCommandValidation()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("UnitPrice must be greater than 0.");

        RuleFor(x => x.orderId).NotEmpty().WithMessage("OrderId is required.");

        RuleFor(x => x.productId).NotEmpty().WithMessage("ProductId is required.");
    }
}
