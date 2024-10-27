using Application.Commands.OrderDetail.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.OrderDetail.GetById;

public record GetByIdOrderDetailCommand(Guid Id)
    : IRequest<ErrorOr<ResponseGetByIdOrderDetailDto>>;
