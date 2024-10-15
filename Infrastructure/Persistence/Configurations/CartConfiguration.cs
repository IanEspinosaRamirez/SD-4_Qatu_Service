using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Carts;
using Domain.Entities.Users;

namespace Infrastructure.Persistence.Configuration;

public class CartConfiguration : IEntityTypeConfiguration<Cart> {
  public void Configure(EntityTypeBuilder<Cart> builder) {
    // Table configuration
    builder.ToTable("Carts");

    // Primary key configuration
    builder.HasKey(c => c.Id);

    // Required properties
    builder.Property(c => c.UserId).IsRequired();

    // Relationship configurations

    // Foreign key for User (1 Cart -> 1 User)
    builder
        .HasOne<User>() // No need to reference the User navigation property
        .WithOne()
        .HasForeignKey<Cart>(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade); // When a user is deleted, the
                                           // related cart is also deleted

    // One-to-many relationship with CartItems (Cart -> CartItems)
    builder.HasMany(c => c.CartItems)
        .WithOne() // No need to reference the Cart navigation property
        .HasForeignKey(ci => ci.CartId)
        .OnDelete(DeleteBehavior.Cascade); // When a cart is deleted, related
                                           // cart items are also deleted
  }
}
