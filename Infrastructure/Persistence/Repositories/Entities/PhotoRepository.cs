using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Photos;

namespace Infrastructure.Persistence.Repositories.Entities;

public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository {
  public PhotoRepository(ApplicationDbContext context) : base(context) {}

  public async Task<IEnumerable<Photo>> GetPhotosByProductId(string productId) {
    var productCustomerId = new CustomerId(Guid.Parse(productId));
    return await _context.Photos
        .Where(photo => photo.ProductId == productCustomerId)
        .ToListAsync();
  }

  public async Task<IEnumerable<Photo>> GetPhotosByStoreId(string storeId) {
    var storeCustomerId = new CustomerId(Guid.Parse(storeId));
    return await _context.Photos
        .Where(photo => photo.StoreId == storeCustomerId)
        .ToListAsync();
  }
}
