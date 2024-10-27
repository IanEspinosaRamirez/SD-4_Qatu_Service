using FluentValidation;

namespace Application.Commands.ReviewProduct.Update;

public class UpdateReviewProductCommandValidator
    : AbstractValidator<UpdateReviewProductCommand> {
  public UpdateReviewProductCommandValidator() {
    RuleFor(command => command.Id)
        .NotEmpty()
        .WithMessage("Id is required.")
        .Must(id => Guid.TryParse(id.ToString(), out _))
        .WithMessage("Invalid UUID format.");

    RuleFor(command => command.Rating)
        .NotEmpty()
        .WithMessage("Rating is required.")
        .InclusiveBetween(1, 5)
        .WithMessage("Rating must be between 1 and 5.");

    RuleFor(command => command.Content)
        .NotEmpty()
        .WithMessage("Content is required.")
        .MaximumLength(500)
        .WithMessage("Content must not exceed 500 characters.");
  }
}