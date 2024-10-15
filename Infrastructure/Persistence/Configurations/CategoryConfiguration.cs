using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Categories;

namespace Infrastructure.Persistence.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category> {
  public void Configure(EntityTypeBuilder<Category> builder) {
    // Table configuration
    builder.ToTable("Categories");

    // Primary key configuration
    builder.HasKey(c => c.Id);

    // Required properties
    builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

    // Relationship configurations

    // One-to-many relationship with Products
    builder.HasMany(c => c.Products)
        .WithOne() // No need to reference the Product navigation property
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Cascade); // When a category is deleted,
                                           // related products are deleted

    // One-to-many relationship with Coupons
    builder.HasMany(c => c.Coupons)
        .WithOne() // No need to reference the Coupon navigation property
        .HasForeignKey(coupon => coupon.CategoryId)
        .OnDelete(
            DeleteBehavior
                .SetNull); // Setting foreign key to null if category is deleted
  }
}
