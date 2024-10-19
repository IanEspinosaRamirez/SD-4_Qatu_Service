using Domain.Entities;
using Domain.Primitives;

namespace Infrastructure.Persistence;

public class BaseRepository<T> : IBaseRepository<T>
    where T : AggregateRoot
{
    private readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Add an entity to the context
    public Task Add(T entity) => Task.Run(() => _context.Set<T>().Add(entity));

    // Update an existing entity in the context
    public Task
    Update(T entity) => Task.Run(() => _context.Set<T>().Update(entity));

    // Delete an entity from the context
    public Task
    Delete(T entity) => Task.Run(() => _context.Set<T>().Remove(entity));

    // Get an entity by its ID
    public async Task<T?>
    GetById(CustomerId id) => await _context.Set<T>().FindAsync(id);
}
