using FluentValidation;

namespace Application.Querys.Product.GetById;

public class GetByIdProductQueryValidation
    : AbstractValidator<GetByIdProductQuery> {
  public GetByIdProductQueryValidation() {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
  }
}
