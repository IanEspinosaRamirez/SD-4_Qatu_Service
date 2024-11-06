using Application.Queries.Categories.DTOs;
using ErrorOr;
using MediatR;
using Domain.Primitives;

namespace Application.Queries.Categories.GetAll;

internal sealed class GetAllCategoriesQueryHandler
    : IRequestHandler<GetAllCategoriesQuery,
                      ErrorOr<List<ResponseGetAllCategories>>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<List<ResponseGetAllCategories>>>
  Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken) {
    var categories = await _unitOfWork.CategoryRepository.GetAll();

    if (!categories.Any()) {
      return Error.Failure("Category.NoRecords", "No categories found.");
    }

    var categoryDtos =
        categories
            .Select(category => new ResponseGetAllCategories {
              Id = category.Id.Value.ToString(), Name = category.Name
            })
            .ToList();

    return categoryDtos;
  }
}
