using FluentValidation;

namespace Application.Commands.Store.Delete;

public class DeleteStoreCommandValidator : AbstractValidator<DeleteStoreCommand>
{
    public DeleteStoreCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid UUID format.");
    }
}
