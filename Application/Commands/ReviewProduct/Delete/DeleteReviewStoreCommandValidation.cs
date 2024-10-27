using FluentValidation;

namespace Application.Commands.ReviewProduct.Delete;

public class DeleteReviewStoreCommandValidator
    : AbstractValidator<DeleteReviewProductCommand> {
  public DeleteReviewStoreCommandValidator() {
    RuleFor(command => command.Id)
        .NotEmpty()
        .WithMessage("Id is required.")
        .Must(id => Guid.TryParse(id.ToString(), out _))
        .WithMessage("Invalid UUID format.");
  }
}
