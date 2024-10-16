using Domain.Entities.Categories;

namespace Infrastructure.Persistence.Repositories.Entities;

public class CategoryRepository : BaseRepository<Category>,
                                  ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context) { }

    // Métodos adicionales específicos de Category
}
