using Application.Queries.Cart.Dto;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Cart.GetById;

internal sealed class GetByIdCartQueryHandler
    : IRequestHandler<GetByIdCartQuery, ErrorOr<ResponseGetCartByIdDto>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetByIdCartQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<ResponseGetCartByIdDto>>
  Handle(GetByIdCartQuery command, CancellationToken cancellationToken) {
    var cart =
        await _unitOfWork.CartRepository.GetById(new CustomerId(command.Id));

    if (cart is null) {
      return Error.Failure("Cart.NotFound", "Cart not found.");
    }

    var cartDto = new ResponseGetCartByIdDto { Id = cart.Id.Value };

    return cartDto;
  }
}
