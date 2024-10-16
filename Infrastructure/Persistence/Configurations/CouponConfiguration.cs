using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Coupons;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon> {
  public void Configure(EntityTypeBuilder<Coupon> builder) {
    // Configuración de la tabla
    builder.ToTable("Coupons");

    // Clave primaria
    builder.HasKey(c => c.Id);
    builder.Property(c => c.Id).HasConversion(id => id.Value,
                                              value => new CustomerId(value));

    builder.Property(c => c.DiscountPercentage).IsRequired();
    builder.Property(c => c.ExpirationDate).IsRequired();
    builder.Property(c => c.IsActive).IsRequired();
    builder.Property(c => c.TypeCoupon).IsRequired().HasConversion<string>();

    // Relaciones

    // Relación opcional con Product
    builder.HasOne(c => c.Product)
        .WithMany(p => p.Coupons)
        .HasForeignKey(c => c.ProductId)
        .OnDelete(DeleteBehavior.SetNull);

    // Relación opcional con Category
    builder.HasOne(c => c.Category)
        .WithMany(cat => cat.Coupons)
        .HasForeignKey(c => c.CategoryId)
        .OnDelete(DeleteBehavior.SetNull);

    // Relación opcional con Store
    builder.HasOne(c => c.Store)
        .WithMany(s => s.Coupons)
        .HasForeignKey(c => c.StoreId)
        .OnDelete(DeleteBehavior.SetNull);
  }
}
