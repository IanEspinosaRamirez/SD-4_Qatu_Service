using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Cart.Create;

internal sealed class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCartCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        CreateCartCommand command,
        CancellationToken cancellationToken
    )
    {
        var cart = new Domain.Entities.Carts.Cart(
            new CustomerId(Guid.NewGuid()),
            new CustomerId(command.UserId)
        );

        await _unitOfWork.CartRepository.Add(cart);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
