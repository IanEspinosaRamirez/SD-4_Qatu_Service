using Domain.Primitives;
using System.Linq.Expressions;

namespace Domain.Entities;

public interface IBaseRepository<T>
    where T : AggregateRoot {
  Task<T?> GetById(CustomerId id);
  Task Add(T entity);
  Task Update(T entity);
  Task UpdatePartial(T entity, params string[] updatedProperties);
  Task Delete(CustomerId id);
  Task<List<T>> GetPaged(int pageNumber, int pageSize,
                         string? filterField = null, string? filterValue = null,
                         Expression<Func<T, object>>? orderBy = null,
                         bool ascending = true);
  Task<List<T>> GetAll();
}
