using Domain.Entities.Coupons;

namespace Infrastructure.Persistence.Repositories.Entities;

public class CouponRepository : BaseRepository<Coupon>, ICouponRepository
{
    public CouponRepository(ApplicationDbContext context) : base(context) { }

    // Métodos adicionales específicos de Coupon
}
