using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Stores;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class StoreConfiguration : IEntityTypeConfiguration<Store> {
  public void Configure(EntityTypeBuilder<Store> builder) {
    // Configuración de la tabla
    builder.ToTable("Stores");

    // Clave primaria
    builder.HasKey(s => s.Id);
    builder.Property(s => s.Id).HasConversion(id => id.Value,
                                              value => new CustomerId(value));

    // Propiedades requeridas y restricciones de longitud
    builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
    builder.Property(s => s.Description).HasMaxLength(500);
    builder.Property(s => s.Address).HasMaxLength(200);
    builder.Property(s => s.CreatedAt).IsRequired();
    builder.Property(s => s.UpdatedAt).IsRequired();

    // Relaciones

    // Relación con User
    builder.HasOne(s => s.User)
        .WithMany(u => u.Stores)
        .HasForeignKey(s => s.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con ReviewStore
    builder.HasMany(s => s.ReviewStores)
        .WithOne(rs => rs.Store)
        .HasForeignKey(rs => rs.StoreId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con Product
    builder.HasMany(s => s.Products)
        .WithOne(p => p.Store)
        .HasForeignKey(p => p.StoreId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con Photo
    builder.HasMany(s => s.Photos)
        .WithOne(p => p.Store)
        .HasForeignKey(p => p.StoreId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con Coupon
    builder.HasMany(s => s.Coupons)
        .WithOne(c => c.Store)
        .HasForeignKey(c => c.StoreId)
        .OnDelete(DeleteBehavior.SetNull);
  }
}
