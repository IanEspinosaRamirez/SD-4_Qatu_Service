using FluentValidation;

namespace Application.Commands.ReviewStores.Delete;

public class DeleteReviewStoreCommandValidator
    : AbstractValidator<DeleteReviewStoreCommand> {
  public DeleteReviewStoreCommandValidator() {
    RuleFor(command => command.Id)
        .NotEmpty()
        .WithMessage("Id is required.")
        .Must(id => Guid.TryParse(id.ToString(), out _))
        .WithMessage("Invalid UUID format.");
  }
}
