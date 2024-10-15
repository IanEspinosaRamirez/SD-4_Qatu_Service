using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.CartItems;
using Domain.Entities.Carts;

namespace Infrastructure.Persistence.Configuration;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem> {
  public void Configure(EntityTypeBuilder<CartItem> builder) {
    // Table configuration
    builder.ToTable("CartItems");

    // Primary key configuration
    builder.HasKey(ci => ci.Id);

    // Required properties
    builder.Property(ci => ci.Quantity).IsRequired();

    // Relationship configurations

    // Foreign key for Cart (CartItem -> Cart)
    builder
        .HasOne<Cart>() // No need to reference the Cart navigation property
        .WithMany(c => c.CartItems)
        .HasForeignKey(ci => ci.CartId)
        .OnDelete(DeleteBehavior.Cascade); // When a cart is deleted, related
                                           // cart items are also deleted
  }
}
