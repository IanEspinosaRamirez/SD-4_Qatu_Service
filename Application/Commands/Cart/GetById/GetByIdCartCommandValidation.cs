using Application.Commands.Cart.GetById;
using FluentValidation;

namespace Application.Carts.Commands.GetByIdCart;

public class GetByIdCartCommandValidation : AbstractValidator<GetByIdCartCommand>
{
    public GetByIdCartCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required")
            .NotNull()
            .WithMessage("Id is required");
    }
}
