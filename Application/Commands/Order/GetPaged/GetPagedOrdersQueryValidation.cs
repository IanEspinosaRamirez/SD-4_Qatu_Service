using FluentValidation;

namespace Application.Commands.Order.GetPaged;

public class GetPagedOrdersQueryValidation : AbstractValidator<GetPagedOrdersQuery>
{
    public GetPagedOrdersQueryValidation()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}
