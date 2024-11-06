using Application.Queries.ReviewProduct.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.ReviewProduct.GetById;

public record GetByIdReviewProductQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetReviewProductByIdDto>>;
