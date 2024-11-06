using Application.Queries.Cart.GetById;
using FluentValidation;

namespace Application.Queries.Cart.GetByIdCart;

public class GetByIdCartQueryValidation : AbstractValidator<GetByIdCartQuery>
{
    public GetByIdCartQueryValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required")
            .NotNull()
            .WithMessage("Id is required");
    }
}
