using Application.Querys.ReviewProduct.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Querys.ReviewProduct.GetById;

public record GetByIdReviewProductQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetReviewProductByIdDto>>;
