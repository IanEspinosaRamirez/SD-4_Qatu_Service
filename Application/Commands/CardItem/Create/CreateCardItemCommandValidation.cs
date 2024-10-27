using FluentValidation;

namespace Application.Commands.CartItem.Create;

internal sealed class CreateCartItemCommandValidation
    : AbstractValidator<CreateCartItemCommand> {
  public CreateCartItemCommandValidation() {
    RuleFor(x => x.quantity)
        .NotEmpty()
        .GreaterThan(0)
        .WithMessage("The quantity must be greater than zero.");

    RuleFor(x => x.cartId)
        .NotEmpty()
        .WithMessage("The cartId must not be empty.");
  }
}
