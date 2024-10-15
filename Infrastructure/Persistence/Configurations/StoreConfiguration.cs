using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Stores;
using Domain.Entities.Users;

namespace Infrastructure.Persistence.Configuration;

public class StoreConfiguration : IEntityTypeConfiguration<Store> {
  public void Configure(EntityTypeBuilder<Store> builder) {
    // Table configuration
    builder.ToTable("Stores");

    // Primary key configuration
    builder.HasKey(s => s.Id);

    // Required properties and length restrictions
    builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
    builder.Property(s => s.Description).HasMaxLength(500);
    builder.Property(s => s.Address).HasMaxLength(200);
    builder.Property(s => s.CreatedAt).IsRequired();
    builder.Property(s => s.UpdatedAt).IsRequired();

    // Relationship configurations

    // One-to-many relationship with ReviewStore (optional)
    builder.HasMany(s => s.ReviewStores)
        .WithOne() // No need to reference the Store navigation property
        .HasForeignKey(rs => rs.StoreId)
        .OnDelete(DeleteBehavior.Cascade);

    // Required foreign key for User (use only UserId)
    builder
        .HasOne<User>() // No need to reference the User navigation property
        .WithMany(u => u.Stores)
        .HasForeignKey(s => s.UserId)
        .OnDelete(
            DeleteBehavior.Cascade); // Deletes the store if the user is deleted

    // One-to-many relationship with Product
    builder.HasMany(s => s.Products)
        .WithOne() // No need to reference the Store navigation property
        .HasForeignKey(p => p.StoreId)
        .OnDelete(DeleteBehavior.Cascade);

    // One-to-many relationship with Photo (required)
    builder.HasMany(s => s.Photos)
        .WithOne() // No need to reference the Store navigation property
        .HasForeignKey(p => p.StoreId)
        .OnDelete(DeleteBehavior.Cascade);

    // Optional relationship with Coupon
    builder.HasMany(s => s.Coupons)
        .WithOne() // No need to reference the Store navigation property
        .HasForeignKey(c => c.StoreId)
        .OnDelete(DeleteBehavior
                      .SetNull); // Set StoreId to null when store is deleted
  }
}
