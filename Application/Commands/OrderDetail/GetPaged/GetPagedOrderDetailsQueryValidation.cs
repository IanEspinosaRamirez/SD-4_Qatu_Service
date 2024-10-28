using FluentValidation;

namespace Application.Commands.OrderDetail.GetPaged;

public sealed class GetPagedOrderDetailsQueryValidation
    : AbstractValidator<GetPagedOrderDetailsQuery>
{
    public GetPagedOrderDetailsQueryValidation()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.");
    }
}
