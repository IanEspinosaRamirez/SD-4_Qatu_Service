using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Coupons;

namespace Infrastructure.Persistence.Configuration;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon> {
  public void Configure(EntityTypeBuilder<Coupon> builder) {
    // Table configuration
    builder.ToTable("Coupons");

    // Primary key configuration
    builder.HasKey(c => c.Id);

    // Required properties
    builder.Property(c => c.DiscountPercentage).IsRequired();

    builder.Property(c => c.ExpirationDate).IsRequired();

    builder.Property(c => c.IsActive).IsRequired();

    builder.Property(c => c.TypeCoupon)
        .IsRequired()
        .HasConversion<string>(); // Convert enum to string for storage

    // Optional foreign key for Product
    builder.HasOne(c => c.Product)
        .WithMany(p => p.Coupons)
        .HasForeignKey(c => c.ProductId)
        .OnDelete(
            DeleteBehavior
                .SetNull); // Set foreign key to null if product is deleted

    // Optional foreign key for Category
    builder.HasOne(c => c.Category)
        .WithMany(cat => cat.Coupons)
        .HasForeignKey(c => c.CategoryId)
        .OnDelete(
            DeleteBehavior
                .SetNull); // Set foreign key to null if category is deleted

    // Optional foreign key for Store
    builder.HasOne(c => c.Store)
        .WithMany(s => s.Coupons)
        .HasForeignKey(c => c.StoreId)
        .OnDelete(DeleteBehavior
                      .SetNull); // Set foreign key to null if store is deleted
  }
}
