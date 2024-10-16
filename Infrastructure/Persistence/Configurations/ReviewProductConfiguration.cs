using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.ReviewProducts;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class ReviewProductConfiguration
    : IEntityTypeConfiguration<ReviewProduct> {
  public void Configure(EntityTypeBuilder<ReviewProduct> builder) {
    // Configuración de la tabla
    builder.ToTable("ReviewProducts");

    // Clave primaria
    builder.HasKey(rp => rp.Id);
    builder.Property(rp => rp.Id)
        .HasConversion(id => id.Value, value => new CustomerId(value));

    builder.Property(rp => rp.Rating).IsRequired();
    builder.Property(rp => rp.Content).IsRequired().HasMaxLength(500);
    builder.Property(rp => rp.CreatedAt).IsRequired();

    // Relaciones

    // Relación con User
    builder.HasOne(rp => rp.User)
        .WithMany(u => u.ReviewProducts)
        .HasForeignKey(rp => rp.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación con Product
    builder.HasOne(rp => rp.Product)
        .WithMany(p => p.ReviewProducts)
        .HasForeignKey(rp => rp.ProductId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
