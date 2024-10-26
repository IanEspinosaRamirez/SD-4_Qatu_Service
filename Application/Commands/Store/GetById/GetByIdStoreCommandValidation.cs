using FluentValidation;

namespace Application.Commands.Store.GetById;

public class GetByIdStoreCommandValidator
    : AbstractValidator<GetByIdStoreCommand>
{
    public GetByIdStoreCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid UUID format.");
    }
}
