using FluentValidation;

namespace Application.Commands.Order.Delete;

public sealed class DeleteOrderCommandValidation : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidation()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}
