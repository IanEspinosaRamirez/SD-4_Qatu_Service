using FluentValidation;

namespace Application.Commands.Store.Create;

internal sealed class CreateStoreCommandValidation : AbstractValidator<CreateStoreCommand>
{
    public CreateStoreCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.Address).NotEmpty().MaximumLength(500).WithMessage("Address must not exceed 500 characters.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
