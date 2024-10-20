using FluentValidation;

namespace Application.Commands.User.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        // Validar que el Id sea requerido y válido
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("User ID is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid UUID format.");

        // Validación para el nombre completo
        RuleFor(command => command.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(100)
            .WithMessage("Full name must not exceed 100 characters.");

        // Validación para el nombre de usuario
        RuleFor(command => command.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MaximumLength(50)
            .WithMessage("Username must not exceed 50 characters.");

        // Validación para el correo electrónico
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        // Validación para el teléfono (opcional)
        RuleFor(command => command.Phone)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits.")
            .When(command => !string.IsNullOrEmpty(command.Phone));

        // Validación para el país
        RuleFor(command => command.Country)
            .NotEmpty()
            .WithMessage("Country is required.")
            .MaximumLength(50)
            .WithMessage("Country must not exceed 50 characters.");

        // Validación para la dirección
        RuleFor(command => command.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MaximumLength(200)
            .WithMessage("Address must not exceed 200 characters.");
    }
}
