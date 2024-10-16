using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Products;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product> {
  public void Configure(EntityTypeBuilder<Product> builder) {
    // Configuración de la tabla
    builder.ToTable("Products");

    // Clave primaria
    builder.HasKey(p => p.Id);
    builder.Property(p => p.Id).HasConversion(id => id.Value,
                                              value => new CustomerId(value));

    // Propiedades requeridas y restricciones de longitud
    builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
    builder.Property(p => p.Price).IsRequired();
    builder.Property(p => p.Description).HasMaxLength(500).IsRequired();
    builder.Property(p => p.Stock).IsRequired();
    builder.Property(p => p.Brand).HasMaxLength(50).IsRequired();
    builder.Property(p => p.CreatedAt).IsRequired();
    builder.Property(p => p.UpdatedAt).IsRequired();
    builder.Property(p => p.Status).IsRequired().HasConversion<string>();

    // Relaciones

    // Relación con Store
    builder.HasOne(p => p.Store)
        .WithMany(s => s.Products)
        .HasForeignKey(p => p.StoreId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación con Category
    builder.HasOne(p => p.Category)
        .WithMany(c => c.Products)
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict

    // Relación uno a muchos con ReviewProduct
    builder.HasMany(p => p.ReviewProducts)
        .WithOne(rp => rp.Product)
        .HasForeignKey(rp => rp.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con OrderDetail
    builder.HasMany(p => p.OrderDetails)
        .WithOne(od => od.Product)
        .HasForeignKey(od => od.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con Photo
    builder.HasMany(p => p.Photos)
        .WithOne(ph => ph.Product)
        .HasForeignKey(ph => ph.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con Coupon
    builder.HasMany(p => p.Coupons)
        .WithOne(c => c.Product)
        .HasForeignKey(c => c.ProductId)
        .OnDelete(DeleteBehavior.SetNull);
  }
}
