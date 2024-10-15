using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Users;
using Domain.Entities.Carts;

namespace Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User> {
  public void Configure(EntityTypeBuilder<User> builder) {
    // Table configuration
    builder.ToTable("Users");

    // Primary key configuration
    builder.HasKey(u => u.Id);

    // Required properties and length restrictions
    builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);

    builder.Property(u => u.Email).IsRequired().HasMaxLength(100);

    builder.HasIndex(u => u.Email).IsUnique(); // Add unique index on Email

    // Nullable Phone property with unique constraint
    builder.Property(u => u.Phone).HasMaxLength(15);
    builder.HasIndex(u => u.Phone).IsUnique(); // Add unique index on Phone

    builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
    builder.HasIndex(u => u.Username)
        .IsUnique(); // Add unique index on Username

    builder.Property(u => u.Password).IsRequired();
    builder.Property(u => u.Country).HasMaxLength(50).IsRequired();
    builder.Property(u => u.CreatedAt).IsRequired();
    builder.Property(u => u.UpdatedAt).IsRequired();
    builder.Property(u => u.ActiveAccount).IsRequired();
    builder.Property(u => u.VerifiedAccount).IsRequired();
    builder.Property(u => u.Address).HasMaxLength(200).IsRequired();

    // Nullable ImageURL property
    builder.Property(u => u.ImageURL).HasMaxLength(200);

    // UserRole configuration
    builder.Property(u => u.RoleUser).IsRequired().HasConversion<string>();

    // Relationship configurations

    // One-to-many relationship with ReviewStore (optional)
    builder.HasMany(u => u.ReviewStores)
        .WithOne() // No need to reference the User navigation property
        .HasForeignKey(rs => rs.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // One-to-many relationship with ReviewProduct (optional)
    builder.HasMany(u => u.ReviewProducts)
        .WithOne() // No need to reference the User navigation property
        .HasForeignKey(rp => rp.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // One-to-many relationship with Store (optional)
    builder.HasMany(u => u.Stores)
        .WithOne() // No need to reference the User navigation property
        .HasForeignKey(s => s.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // One-to-one relationship with Cart (optional)
    builder
        .HasOne<Cart>() // No need to reference the User navigation property
        .WithOne()
        .HasForeignKey<Cart>(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // One-to-many relationship with Order (optional)
    builder.HasMany(u => u.Orders)
        .WithOne() // No need to reference the User navigation property
        .HasForeignKey(o => o.UserId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
