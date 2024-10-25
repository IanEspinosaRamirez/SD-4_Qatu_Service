using Application.Commands.Product.Delete;
using FluentValidation;

namespace Application.Commands.Prouduct.Delete;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}
