using FluentValidation;

namespace Application.Commands.User.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Id is required.");
    }
}
