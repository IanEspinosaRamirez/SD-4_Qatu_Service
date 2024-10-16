using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.ReviewStores;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class ReviewStoreConfiguration : IEntityTypeConfiguration<ReviewStore> {
  public void Configure(EntityTypeBuilder<ReviewStore> builder) {
    // Configuración de la tabla
    builder.ToTable("ReviewStores");

    // Clave primaria
    builder.HasKey(rs => rs.Id);
    builder.Property(rs => rs.Id)
        .HasConversion(id => id.Value, value => new CustomerId(value));

    builder.Property(rs => rs.Rating).IsRequired();
    builder.Property(rs => rs.Content).IsRequired().HasMaxLength(500);
    builder.Property(rs => rs.CreatedAt).IsRequired();

    // Relaciones

    // Relación con User
    builder.HasOne(rs => rs.User)
        .WithMany(u => u.ReviewStores)
        .HasForeignKey(rs => rs.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación con Store
    builder.HasOne(rs => rs.Store)
        .WithMany(s => s.ReviewStores)
        .HasForeignKey(rs => rs.StoreId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
