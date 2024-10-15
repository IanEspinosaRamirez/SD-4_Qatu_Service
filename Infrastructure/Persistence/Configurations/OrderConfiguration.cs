using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Orders;

namespace Infrastructure.Persistence.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order> {
  public void Configure(EntityTypeBuilder<Order> builder) {
    // Table configuration
    builder.ToTable("Orders");

    // Primary key configuration
    builder.HasKey(o => o.Id);

    // Required properties and length restrictions
    builder.Property(o => o.TotalPrice).IsRequired();

    builder.Property(o => o.ShippingMethod).IsRequired().HasMaxLength(100);

    builder.Property(o => o.PaymentMethod).IsRequired().HasMaxLength(100);

    builder.Property(o => o.OrderDate).IsRequired();

    // Relationship configurations

    // Foreign key for User (Order -> User)
    builder.HasOne(o => o.User)
        .WithMany(u => u.Orders)
        .HasForeignKey(o => o.UserId)
        .OnDelete(
            DeleteBehavior
                .Cascade); // When a User is deleted, related orders are deleted

    // One-to-many relationship with OrderDetail
    builder.HasMany(o => o.OrderDetails)
        .WithOne(od => od.Order)
        .HasForeignKey(od => od.OrderId)
        .OnDelete(DeleteBehavior.Cascade); // When an Order is deleted, related
                                           // order details are deleted
  }
}
