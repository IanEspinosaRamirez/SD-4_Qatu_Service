using Application.Commands.Product.GetById;
using Application.Commands.Prouduct.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Prouduct.GetById;

internal sealed class GetByIdProductCommandHandler
    : IRequestHandler<GetByIdProductCommand, ErrorOr<ResponseGetProductByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetProductByIdDto>> Handle(
        GetByIdProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var product = await _unitOfWork.ProductRepository.GetById(new CustomerId(command.Id));

        if (product is null)
        {
            return Error.Failure("Product.NotFound", "Product not found.");
        }

        var productDto = new ResponseGetProductByIdDto
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            Brand = product.Brand,
        };

        return productDto;
    }
}
