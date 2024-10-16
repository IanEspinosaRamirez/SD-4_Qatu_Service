using Domain.Entities.Photos;

namespace Infrastructure.Persistence.Repositories.Entities;

public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository {
  public PhotoRepository(ApplicationDbContext context) : base(context) {}

  // Aquí puedes agregar métodos adicionales específicos de Photo si es
  // necesario
}
