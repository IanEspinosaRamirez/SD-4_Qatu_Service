using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.Create;

internal sealed class CreateCardItemCommandHandler
    : IRequestHandler<CreateCartItemCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public CreateCardItemCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(CreateCartItemCommand command,
                                          CancellationToken cancellationToken) {
    var cartItem = new Domain.Entities.CartItems.CartItem(
        new CustomerId(Guid.NewGuid()), command.quantity, command.cartId);

    await _unitOfWork.CartItemRepository.Add(cartItem);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
