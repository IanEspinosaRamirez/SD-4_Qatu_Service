using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Categories;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category> {
  public void Configure(EntityTypeBuilder<Category> builder) {
    // Configuración de la tabla
    builder.ToTable("Categories");

    // Clave primaria
    builder.HasKey(c => c.Id);
    builder.Property(c => c.Id).HasConversion(id => id.Value,
                                              value => new CustomerId(value));

    builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

    // Relaciones

    // Relación uno a muchos con Product
    builder.HasMany(c => c.Products)
        .WithOne(p => p.Category)
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.SetNull);

    // Relación uno a muchos con Coupon
    builder.HasMany(c => c.Coupons)
        .WithOne(coupon => coupon.Category)
        .HasForeignKey(coupon => coupon.CategoryId)
        .OnDelete(DeleteBehavior.SetNull);
  }
}
