namespace Domain.Entities;

public interface IBaseRepository<T>
    where T : class {

  Task<T?> GetById(CustomerId id);
  Task Add(T entity);
  Task Update(T entity);
  Task Delete(T entity);
}
