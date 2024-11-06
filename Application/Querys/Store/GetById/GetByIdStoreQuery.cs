using ErrorOr;
using MediatR;

namespace Application.Querys.Store.GetById;

public record GetByIdStoreQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetStoreByIdDto>>;
