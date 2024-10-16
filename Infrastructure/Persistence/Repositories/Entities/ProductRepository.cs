using Domain.Entities.Products;

namespace Infrastructure.Persistence.Repositories.Entities;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) { }

    // Métodos adicionales específicos de Product
}
