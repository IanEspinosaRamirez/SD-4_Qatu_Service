using FluentValidation;

namespace Application.Queries.Product.GetPaged;

public sealed class GetProductsPagedQueryValidation
    : AbstractValidator<GetProductsPagedQuery>
{
    public GetProductsPagedQueryValidation()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");
    }
}
