using Domain.Entities;

namespace Domain.Primitives;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> BaseRepository<T>()
        where T : AggregateRoot;

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
