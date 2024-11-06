using ErrorOr;
using MediatR;

namespace Application.Queries.Store.GetById;

public record GetByIdStoreQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetStoreByIdDto>>;
