using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Product.Create;

internal sealed class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<ErrorOr<Unit>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Products.Product(
            new CustomerId(Guid.NewGuid()),
            command.Name,
            command.Price,
            command.Description,
            command.Stock,
            command.Brand,
            command.storeId,
            command.categoryId
        );

        await _unitOfWork.ProductRepository.Add(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}
