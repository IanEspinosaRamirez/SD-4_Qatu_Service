using Application.Queries.Category.Dtos;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Category.GetById;

internal sealed class GetByIdCategoryQueryHandler
    : IRequestHandler<GetByIdCategoryQuery,
                      ErrorOr<ResponseGetCategoryByIdDto>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetByIdCategoryQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<ResponseGetCategoryByIdDto>>
  Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken) {
    var category = await _unitOfWork.CategoryRepository.GetById(
        new CustomerId(request.Id));
    if (category is null) {
      return Error.Failure("Category.NotFound", "Category not found.");
    }

    var categoryDto = new ResponseGetCategoryByIdDto {
      Name = category.Name,
    };

    return categoryDto;
  }
}
