using Application.Commands.Category.Dtos;
using Application.Commands.User.GetById;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Category.GetById;

internal sealed class GetByIdCategoryCommandHandler
    : IRequestHandler<GetByIdCategoryCommand, ErrorOr<ResponseGetCategoryByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetCategoryByIdDto>> Handle(GetByIdCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetById(new CustomerId(request.Id));
        if (category is null)
        {
            return Error.Failure("Category.NotFound", "Category not found.");
        }

        var categoryDto = new ResponseGetCategoryByIdDto
        {
            Name = category.Name,
        };

        return categoryDto;


    }
}
