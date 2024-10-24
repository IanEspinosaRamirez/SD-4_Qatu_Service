using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Category.Update;


internal sealed class UpdateCategoryCommandHandler
    : IRequestHandler<UpdateCategoryCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Categories.Category(
            new CustomerId(Guid.Parse(command.Id)), command.Name);

        await _unitOfWork.CategoryRepository.Update(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}

