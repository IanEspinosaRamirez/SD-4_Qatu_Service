using FluentValidation;

namespace Application.Querys.Store.GetById;

public class GetByIdStoreQueryValidator : AbstractValidator<GetByIdStoreQuery> {
  public GetByIdStoreQueryValidator() {
    RuleFor(command => command.Id).NotEmpty().WithMessage("Id is required.");
  }
}
