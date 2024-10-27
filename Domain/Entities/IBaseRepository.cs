using Domain.Primitives;

namespace Domain.Entities;

public interface IBaseRepository<T>
    where T : AggregateRoot {
  Task<T?> GetById(CustomerId id);
  Task Add(T entity);
  Task Update(T entity);
  Task UpdatePartial(T entity, params string[] updatedProperties);
  Task Delete(CustomerId id);
}
