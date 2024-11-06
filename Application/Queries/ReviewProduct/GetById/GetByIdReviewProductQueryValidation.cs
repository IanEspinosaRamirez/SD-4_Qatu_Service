using FluentValidation;

namespace Application.Queries.ReviewProduct.GetById;

public class GetByIdReviewProductQueryValidator
    : AbstractValidator<GetByIdReviewProductQuery>
{
    public GetByIdReviewProductQueryValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Id is required.");
    }
}
