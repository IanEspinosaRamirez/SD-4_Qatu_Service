using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.CartItems;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem> {
  public void Configure(EntityTypeBuilder<CartItem> builder) {
    // Configuración de la tabla
    builder.ToTable("CartItems");

    // Clave primaria
    builder.HasKey(ci => ci.Id);
    builder.Property(ci => ci.Id)
        .HasConversion(id => id.Value, value => new CustomerId(value));

    builder.Property(ci => ci.Quantity).IsRequired();

    // Relaciones

    // Relación con Cart
    builder.HasOne(ci => ci.Cart)
        .WithMany(c => c.CartItems)
        .HasForeignKey(ci => ci.CartId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
