using FluentValidation;

namespace Application.Commands.Product.GetById;

public class GetByIdProductCommandValidation : AbstractValidator<GetByIdProductCommand>
{
    public GetByIdProductCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
