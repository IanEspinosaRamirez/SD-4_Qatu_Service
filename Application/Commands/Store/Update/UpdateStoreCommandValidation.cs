/*using FluentValidation;*/
/**/
/*namespace Application.Commands.Store.Update;*/
/**/
/*public class UpdateStoreCommandValidator : AbstractValidator<UpdateStoreCommand>*/
/*{*/
/*    public UpdateStoreCommandValidator()*/
/*    {*/
/*        // Validar que el Id sea requerido y válido*/
/*        RuleFor(command => command.Id)*/
/*            .NotEmpty()*/
/*            .WithMessage("Store ID is required.")*/
/*            .Must(id => Guid.TryParse(id.ToString(), out _))*/
/*            .WithMessage("Invalid UUID format.");*/
/**/
/*        // Validación para el nombre*/
/*        RuleFor(command => command.Name)*/
/*            .NotEmpty()*/
/*            .WithMessage("Name is required.")*/
/*            .MaximumLength(100)*/
/*            .WithMessage("Name must not exceed 100 characters.");*/
/**/
/*        // Validación para la descripción*/
/*        RuleFor(command => command.Description)*/
/*            .NotEmpty()*/
/*            .WithMessage("Description is required.")*/
/*            .MaximumLength(200)*/
/*            .WithMessage("Description must not exceed 200 characters.");*/
/**/
/*        // Validación para la dirección*/
/*        RuleFor(command => command.Address)*/
/*            .NotEmpty()*/
/*            .WithMessage("Address is required.")*/
/*            .MaximumLength(200)*/
/*            .WithMessage("Address must not exceed 200 characters.");*/
/*    }*/
/**/
/*}*/
