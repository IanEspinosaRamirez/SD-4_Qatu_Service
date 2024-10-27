using Application.Commands.ReviewStore.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStore.GetById;

public record GetByIdReviewStoreCommand(Guid Id)
    : IRequest<ErrorOr<ResponseGetReviewStoreByIdDto>>;
