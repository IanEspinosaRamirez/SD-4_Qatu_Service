using FluentValidation;

namespace Application.Queries.Store.GetPaged;

public sealed class GetStoresPagedQueryValidation
    : AbstractValidator<GetStoresPagedQuery>
{
    public GetStoresPagedQueryValidation()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");
    }
}
