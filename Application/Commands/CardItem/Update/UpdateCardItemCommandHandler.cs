using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.Update;

internal sealed class UpdateCardItemCommandHandler
    : IRequestHandler<UpdateCartItemCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public UpdateCardItemCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(UpdateCartItemCommand command,
                                          CancellationToken cancellationToken) {

    var cartItem = new Domain.Entities.CartItems.CartItem(
        new CustomerId(command.Id), command.Quantity,
        new CustomerId(command.Id));

    await _unitOfWork.CartItemRepository.UpdatePartial(
        cartItem, nameof(cartItem.Quantity));

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
