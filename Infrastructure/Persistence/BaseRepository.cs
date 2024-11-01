using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Primitives;
using System.Linq.Expressions;

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

  public async Task<List<T>>
  GetPaged(int pageNumber, int pageSize, string? filterField = null,
           string? filterValue = null,
           Expression<Func<T, object>>? orderBy = null, bool ascending = true) {
    IQueryable<T> query = _context.Set<T>();

    if (!string.IsNullOrEmpty(filterField) &&
        !string.IsNullOrEmpty(filterValue)) {
      var parameter = Expression.Parameter(typeof(T), "x");
      var property = Expression.Property(parameter, filterField);
      var constant = Expression.Constant(filterValue);
      var containsMethod =
          typeof(string).GetMethod("Contains", new[] { typeof(string) });
      var containsExpression =
          Expression.Call(property, containsMethod!, constant);
      var lambda =
          Expression.Lambda<Func<T, bool>>(containsExpression, parameter);

      query = query.Where(lambda);
    }

    if (orderBy != null) {
      query =
          ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
    }

    return await query.Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
  }
  public async Task<List<T>> GetAll() {
    return await _context.Set<T>().ToListAsync();
  }
}
