using Application.Commands.OrderDetail.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.OrderDetail.GetById;

internal sealed class GetByIdOrderDetailCommandHandler
    : IRequestHandler<GetByIdOrderDetailCommand,
                      ErrorOr<ResponseGetByIdOrderDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdOrderDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetByIdOrderDetailDto>>
    Handle(GetByIdOrderDetailCommand command,
           CancellationToken cancellationToken)
    {
        var orderDetail = await _unitOfWork.OrderDetailRepository.GetById(
            new CustomerId(command.Id));

        if (orderDetail is null)
        {
            return Error.Failure("Store.NotFound", "Store not found.");
        }

        var orderDetailDto =
            new ResponseGetByIdOrderDetailDto
            {
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice
            };

        return orderDetailDto;
    }
}
