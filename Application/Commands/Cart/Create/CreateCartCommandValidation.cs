using FluentValidation;

namespace Application.Commands.Cart.Create;

internal sealed class CreateCartCommandValidation : AbstractValidator<CreateCartCommand>
{
    public CreateCartCommandValidation()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
