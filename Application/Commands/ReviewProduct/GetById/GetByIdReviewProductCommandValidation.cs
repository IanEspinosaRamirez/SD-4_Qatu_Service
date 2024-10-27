using FluentValidation;

namespace Application.Commands.ReviewProduct.GetById;

public class GetByIdReviewProductCommandValidator
    : AbstractValidator<GetByIdReviewProductCommand> {
  public GetByIdReviewProductCommandValidator() {
    RuleFor(command => command.Id)
        .NotEmpty()
        .WithMessage("Id is required.")
        .Must(id => Guid.TryParse(id.ToString(), out _))
        .WithMessage("Invalid UUID format.");
  }
}