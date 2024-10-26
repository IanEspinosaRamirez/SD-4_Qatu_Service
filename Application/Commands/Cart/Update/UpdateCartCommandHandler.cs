using Domain.Entities;
using Domain.Primitives;
/*using ErrorOr;*/
/*using MediatR;*/
/**/
/*namespace Application.Commands.Cart.Update;*/
/**/
/*internal sealed class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, ErrorOr<Unit>>*/
/*{*/
/*    private readonly IUnitOfWork _unitOfWork;*/
/**/
/*    public UpdateCartCommandHandler(IUnitOfWork unitOfWork)*/
/*    {*/
/*        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));*/
/*    }*/
/**/
/*    public async Task<ErrorOr<Unit>> Handle(*/
/*        UpdateCartCommand command,*/
/*        CancellationToken cancellationToken*/
/*    )*/
/*    {*/
/*        var cart = new Domain.Entities.Carts.Cart(new CustomerId(command.UserId));*/
/**/
/*        await _unitOfWork.CartRepository.Update(cart);*/
/**/
/*        await _unitOfWork.SaveChangesAsync(cancellationToken);*/
/**/
/*        return Unit.Value;*/
/*    }*/
/*}*/
