using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.ReviewProducts;
using Domain.Entities.Users;
using Domain.Entities.Products;

namespace Infrastructure.Persistence.Configuration;

public class ReviewProductConfiguration
    : IEntityTypeConfiguration<ReviewProduct> {
  public void Configure(EntityTypeBuilder<ReviewProduct> builder) {
    // Table configuration
    builder.ToTable("ReviewProducts");

    // Primary key configuration
    builder.HasKey(rp => rp.Id);

    // Required properties and length restrictions
    builder.Property(rp => rp.Rating).IsRequired();

    builder.Property(rp => rp.Content).IsRequired().HasMaxLength(1000);

    builder.Property(rp => rp.CreatedAt).IsRequired();

    // Relationship configurations

    // Foreign key for User (ReviewProduct -> User)
    builder
        .HasOne<User>() // No need to reference the User property
        .WithMany(u => u.ReviewProducts)
        .HasForeignKey(rp => rp.UserId)
        .OnDelete(DeleteBehavior.Cascade); // When a User is deleted, related
                                           // reviews are deleted as well

    // Foreign key for Product (ReviewProduct -> Product)
    builder
        .HasOne<Product>() // No need to reference the Product property
        .WithMany(p => p.ReviewProducts)
        .HasForeignKey(rp => rp.ProductId)
        .OnDelete(DeleteBehavior.Cascade); // When a Product is deleted, related
                                           // reviews are deleted as well
  }
}
