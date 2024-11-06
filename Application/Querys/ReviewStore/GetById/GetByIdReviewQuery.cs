using Application.Querys.ReviewStore.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Querys.ReviewStore.GetById;

public record GetByIdReviewStoreQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetReviewStoreByIdDto>>;
