using Domain.Entities;
using Domain.Entities.Stores;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories.Entities;

public class StoreRepository : BaseRepository<Store>, IStoreRepository {
  public StoreRepository(ApplicationDbContext context) : base(context) {}

  public async Task<List<Store>>
  GetStoresByUserPaged(string userId, int pageNumber, int pageSize,
                       string? filterField = null, string? filterValue = null,
                       Expression<Func<Store, object>>? orderBy = null,
                       bool ascending = true) {

    IQueryable<Store> query = _context.Set<Store>().Where(
        store => store.UserId == new CustomerId(Guid.Parse(userId)));

    if (!string.IsNullOrEmpty(filterField) &&
        !string.IsNullOrEmpty(filterValue)) {
      var parameter = Expression.Parameter(typeof(Store), "x");
      var property = Expression.Property(parameter, filterField);
      var constant = Expression.Constant(filterValue);
      var containsMethod =
          typeof(string).GetMethod("Contains", new[] { typeof(string) });
      var containsExpression =
          Expression.Call(property, containsMethod!, constant);
      var lambda =
          Expression.Lambda<Func<Store, bool>>(containsExpression, parameter);

      query = query.Where(lambda);
    }

    if (orderBy != null) {
      query =
          ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
    }

    // Paginación
    return await query.Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
  }
}
