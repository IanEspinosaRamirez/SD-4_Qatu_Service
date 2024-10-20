using FluentValidation;

namespace Application.Commands.User.GetById;

public class GetByIdUserCommandValidator
    : AbstractValidator<GetByIdUserCommand>
{
    public GetByIdUserCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid UUID format.");
    }
}
