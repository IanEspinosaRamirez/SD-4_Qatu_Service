using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Photos;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo> {
  public void Configure(EntityTypeBuilder<Photo> builder) {
    // Configuración de la tabla
    builder.ToTable("Photos");

    // Clave primaria
    builder.HasKey(p => p.Id);
    builder.Property(p => p.Id).HasConversion(id => id.Value,
                                              value => new CustomerId(value));

    builder.Property(p => p.ImageURL).IsRequired().HasMaxLength(200);

    // Relaciones

    // Relación opcional con Product
    builder.HasOne(p => p.Product)
        .WithMany(p => p.Photos)
        .HasForeignKey(p => p.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación opcional con Store
    builder.HasOne(p => p.Store)
        .WithMany(s => s.Photos)
        .HasForeignKey(p => p.StoreId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
