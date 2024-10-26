using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Product.Update;

internal sealed class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateProductCommand command,
                                            CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Products.Product(
              new CustomerId(command.Id),
              command.Name,
              command.Price,
              command.Description,
              command.Stock,
              command.Brand,
              command.StoreId,
              command.CategoryId
          );
        await _unitOfWork.ProductRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}
