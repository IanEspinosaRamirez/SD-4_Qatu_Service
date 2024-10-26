using FluentValidation;

namespace Application.Commands.Order.GetById;

public class GetByIdOrderCommandValidation : AbstractValidator<GetByIdOrderCommand>
{
    public GetByIdOrderCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
