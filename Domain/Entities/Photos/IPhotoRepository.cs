namespace Domain.Entities.Photos;

public interface IPhotoRepository : IBaseRepository<Photo> {
  Task<IEnumerable<Photo>> GetPhotosByProductId(string productId);
  Task<IEnumerable<Photo>> GetPhotosByStoreId(string storeId);
}
