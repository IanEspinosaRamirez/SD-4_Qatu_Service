using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Carts;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class CartConfiguration : IEntityTypeConfiguration<Cart> {
  public void Configure(EntityTypeBuilder<Cart> builder) {
    // Configuración de la tabla
    builder.ToTable("Carts");

    // Clave primaria
    builder.HasKey(c => c.Id);
    builder.Property(c => c.Id).HasConversion(id => id.Value,
                                              value => new CustomerId(value));

    // Relaciones

    // Relación con User
    builder.HasOne(c => c.User)
        .WithOne(u => u.Cart)
        .HasForeignKey<Cart>(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con CartItem
    builder.HasMany(c => c.CartItems)
        .WithOne(ci => ci.Cart)
        .HasForeignKey(ci => ci.CartId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
