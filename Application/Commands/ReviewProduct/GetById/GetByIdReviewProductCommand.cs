using Application.Commands.ReviewProduct.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewProduct.GetById;

public record GetByIdReviewProductCommand(Guid Id)
    : IRequest<ErrorOr<ResponseGetReviewProductByIdDto>>;
