using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Category.Create;

internal sealed class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Categories.Category(
            new CustomerId(Guid.NewGuid()), command.Name);

        await _unitOfWork.CategoryRepository.Add(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}
