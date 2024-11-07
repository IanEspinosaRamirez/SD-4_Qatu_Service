using System.Linq.Expressions;

namespace Domain.Entities.Stores;

public interface IStoreRepository : IBaseRepository<Store> {
  Task<List<Store>>
  GetStoresByUserPaged(string userId, int pageNumber, int pageSize,
                       string? filterField = null, string? filterValue = null,
                       Expression<Func<Store, object>>? orderBy = null,
                       bool ascending = true);
}
