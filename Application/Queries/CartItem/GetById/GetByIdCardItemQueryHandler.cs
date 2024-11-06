using Application.Queries.CartItem.GetById.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.CartItem.GetById;

internal sealed class GetByIdCardItemQueryHandler
    : IRequestHandler<GetByIdCartItemQuery,
                      ErrorOr<ResponseGetCartItemByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdCardItemQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetCartItemByIdDto>>
    Handle(GetByIdCartItemQuery query, CancellationToken cancellationToken)
    {

        var cartItem =
            await _unitOfWork.CartItemRepository.GetById(new CustomerId(query.Id));

        if (cartItem is null)
        {
            return Error.Failure("CartItem.NotFound", "CartItem not found.");
        }

        var cartItemDto = new ResponseGetCartItemByIdDto
        {
            Quantity = cartItem.Quantity,
        };

        return cartItemDto;
    }
}
