using FluentValidation;

namespace Application.Queries.Order.GetById;

public class GetByIdOrderQueryValidation
    : AbstractValidator<GetByIdOrderQuery>
{
    public GetByIdOrderQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
