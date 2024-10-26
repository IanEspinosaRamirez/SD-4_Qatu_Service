using FluentValidation;

namespace Application.Commands.Product.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        RuleFor(command => command.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");

        RuleFor(command => command.Price)
            .NotEmpty()
            .WithMessage("Price is required.")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");

        RuleFor(command => command.Stock)
            .NotEmpty()
            .WithMessage("Stock is required.")
            .GreaterThan(0)
            .WithMessage("Stock must be greater than 0.");

        RuleFor(command => command.categoryId).NotEmpty().WithMessage("Category is required.");
    }
}
