using Application.Commands.Cart.Dto;
using Application.Commands.Cart.GetById;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.GetById;

internal sealed class GetByIdCartCommandHandler
    : IRequestHandler<GetByIdCartCommand, ErrorOr<ResponseGetCartByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdCartCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetCartByIdDto>> Handle(
        GetByIdCartCommand command,
        CancellationToken cancellationToken
    )
    {
        var cart = await _unitOfWork.CartRepository.GetById(new CustomerId(command.Id));

        if (cart is null)
        {
            return Error.Failure("Cart.NotFound", "Cart not found.");
        }

        var cartDto = new ResponseGetCartByIdDto { Id = cart.Id.Value };

        return cartDto;
    }
}
