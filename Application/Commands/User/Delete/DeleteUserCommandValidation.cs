using FluentValidation;

namespace Application.Commands.User.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid UUID format.");
    }
}
