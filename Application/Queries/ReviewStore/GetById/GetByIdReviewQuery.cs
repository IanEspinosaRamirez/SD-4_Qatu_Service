using Application.Queries.ReviewStore.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.ReviewStore.GetById;

public record GetByIdReviewStoreQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetReviewStoreByIdDto>>;
