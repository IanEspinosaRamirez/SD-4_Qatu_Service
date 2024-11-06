using FluentValidation;

namespace Application.Querys.ReviewProduct.GetById;

public class GetByIdReviewProductQueryValidator
    : AbstractValidator<GetByIdReviewProductQuery> {
  public GetByIdReviewProductQueryValidator() {
    RuleFor(command => command.Id).NotEmpty().WithMessage("Id is required.");
  }
}
