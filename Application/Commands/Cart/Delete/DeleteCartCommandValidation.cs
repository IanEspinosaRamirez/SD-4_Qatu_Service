using FluentValidation;

namespace Application.Commands.Cart.Delete;

internal sealed class DeleteCartCommandValidation : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
