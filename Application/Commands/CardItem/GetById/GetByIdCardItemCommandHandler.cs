using Application.Commands.CartItem.GetById.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.GetById;

internal sealed class GetByIdCardItemCommandHandler
    : IRequestHandler<GetByIdCartItemCommand,
                      ErrorOr<ResponseGetCartItemByIdDto>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetByIdCardItemCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<ResponseGetCartItemByIdDto>>
  Handle(GetByIdCartItemCommand command, CancellationToken cancellationToken) {

    var cartItem = await _unitOfWork.CartItemRepository.GetById(
        new CustomerId(command.Id));

    if (cartItem is null) {
      return Error.Failure("CartItem.NotFound", "CartItem not found.");
    }

    var cartItemDto = new ResponseGetCartItemByIdDto {
      Quantity = cartItem.Quantity,
    };

    return cartItemDto;
  }
}
