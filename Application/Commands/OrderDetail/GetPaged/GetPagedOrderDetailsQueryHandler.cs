using Application.Commands.OrderDetail.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.OrderDetail.GetPaged;

internal sealed class GetPagedOrderDetailsQueryHandler
    : IRequestHandler<GetPagedOrderDetailsQuery, ErrorOr<List<ResponseGetPagedOrderDetailDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedOrderDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedOrderDetailDto>>> Handle(
        GetPagedOrderDetailsQuery request,
        CancellationToken cancellationToken
    )
    {
        var orderDetails = await _unitOfWork.OrderDetailRepository.GetPaged(
            request.PageNumber,
            request.PageSize
        );

        if (!orderDetails.Any())
        {
            return Error.Failure("OrderDetail.NoRecords", "No order details found.");
        }

        var orderDetailDtos = orderDetails
            .Select(orderDetail => new ResponseGetPagedOrderDetailDto
            {
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice,
            })
            .ToList();

        return orderDetailDtos;
    }
}
