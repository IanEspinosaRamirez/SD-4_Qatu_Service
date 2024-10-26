using ErrorOr;
using MediatR;

namespace Application.Commands.Store.GetById;

public record GetByIdStoreCommand(Guid Id) : IRequest<ErrorOr<ResponseGetStoreByIdDto>>;
