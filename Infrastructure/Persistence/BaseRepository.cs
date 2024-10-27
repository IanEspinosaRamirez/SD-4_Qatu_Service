using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Primitives;

namespace Infrastructure.Persistence;

public class BaseRepository<T> : IBaseRepository<T>
    where T : AggregateRoot {
  public readonly ApplicationDbContext _context;

  public BaseRepository(ApplicationDbContext context) {
    _context = context ?? throw new ArgumentNullException(nameof(context));
  }

  public async Task Add(T entity) {
    if (entity == null)
      throw new ArgumentNullException(nameof(entity));
    await _context.Set<T>().AddAsync(entity);
  }

  public Task Update(T entity) {
    if (entity == null)
      throw new ArgumentNullException(nameof(entity));
    _context.Set<T>().Update(entity);
    return Task.CompletedTask;
  }

  public Task UpdatePartial(T entity, params string[] updatedProperties) {
    if (entity == null)
      throw new ArgumentNullException(nameof(entity));

    var entry = _context.Entry(entity);

    foreach (var property in updatedProperties) {
      entry.Property(property).IsModified = true;
    }

    return Task.CompletedTask;
  }

  public Task Delete(CustomerId id) {
    var entity = Activator.CreateInstance<T>();
    entity.GetType().GetProperty("Id")?.SetValue(entity, id, null);
    _context.Set<T>().Remove(entity);
    return Task.CompletedTask;
  }

  public async Task<T?> GetById(CustomerId id) {
    return await _context.Set<T>().FindAsync(id);
  }

  public async Task<List<T>> GetPaged(int pageNumber, int pageSize) {
    if (pageNumber < 1)
      throw new ArgumentOutOfRangeException(nameof(pageNumber));
    if (pageSize < 1)
      throw new ArgumentOutOfRangeException(nameof(pageSize));

    return await _context.Set<T>()
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
  }
}
