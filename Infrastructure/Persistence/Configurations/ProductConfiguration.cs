using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Products;
using Domain.Entities.Stores;
using Domain.Entities.Categories;

namespace Infrastructure.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product> {
  public void Configure(EntityTypeBuilder<Product> builder) {
    // Table configuration
    builder.ToTable("Products");

    // Primary key configuration
    builder.HasKey(p => p.Id);

    // Required properties and length restrictions
    builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

    builder.Property(p => p.Price).IsRequired();

    builder.Property(p => p.Description).HasMaxLength(1000);

    builder.Property(p => p.Stock).IsRequired();

    builder.Property(p => p.Brand).HasMaxLength(100);

    builder.Property(p => p.CreatedAt).IsRequired();

    builder.Property(p => p.UpdatedAt).IsRequired();

    builder.Property(p => p.Status).HasMaxLength(50);

    // Relationship configurations

    // Foreign key for Store (Product -> Store)
    builder
        .HasOne<Store>() // No need to reference the Store navigation property
        .WithMany(s => s.Products)
        .HasForeignKey(p => p.StoreId)
        .OnDelete(DeleteBehavior.Cascade);

    // Foreign key for Category (Product -> Category)
    builder
        .HasOne<Category>() // No need to reference the Category navigation
                            // property
        .WithMany(c => c.Products)
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Cascade);

    // One-to-many relationship with ReviewProduct
    builder.HasMany(p => p.ReviewProducts)
        .WithOne() // No need to reference the navigation property
        .HasForeignKey(rp => rp.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

    // One-to-many relationship with OrderDetail
    builder.HasMany(p => p.OrderDetails)
        .WithOne() // No need to reference the navigation property
        .HasForeignKey(od => od.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

    // One-to-many relationship with Photo
    builder.HasMany(p => p.Photos)
        .WithOne() // No need to reference the navigation property
        .HasForeignKey(ph => ph.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

    // Optional relationship with Coupon (Product -> Coupon)
    builder.HasMany(p => p.Coupons)
        .WithOne() // No need to reference the navigation property
        .HasForeignKey(c => c.ProductId)
        .OnDelete(
            DeleteBehavior
                .SetNull); // Set ProductId to null when product is deleted
  }
}
