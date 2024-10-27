using FluentValidation;

namespace Application.Commands.ReviewProduct.Create;

internal sealed class CreateReviewProductCommandValidation
    : AbstractValidator<CreateReviewProductCommand> {
  public CreateReviewProductCommandValidation() {
    RuleFor(x => x.rating)
        .InclusiveBetween(1, 5)
        .WithMessage("Rating must be between 1 and 5.");

    RuleFor(x => x.content)
        .NotEmpty()
        .MaximumLength(1000)
        .WithMessage("Content must not exceed 1000 characters.");

    RuleFor(x => x.userId).NotEmpty().WithMessage("UserId is required.");

    RuleFor(x => x.productId).NotEmpty().WithMessage("StoreId is required.");
  }
}
