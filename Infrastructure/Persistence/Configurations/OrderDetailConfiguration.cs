using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.OrderDetails;

namespace Infrastructure.Persistence.Configuration;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail> {
  public void Configure(EntityTypeBuilder<OrderDetail> builder) {
    // Table configuration
    builder.ToTable("OrderDetails");

    // Primary key configuration
    builder.HasKey(od => od.Id);

    // Required properties
    builder.Property(od => od.Quantity).IsRequired();

    builder.Property(od => od.UnitPrice).IsRequired();

    // Relationship configurations

    // Foreign key for Order (OrderDetail -> Order)
    builder.HasOne(od => od.Order)
        .WithMany(o => o.OrderDetails)
        .HasForeignKey(od => od.OrderId)
        .OnDelete(DeleteBehavior.Cascade); // When an Order is deleted, related
                                           // order details are deleted

    // Foreign key for Product (OrderDetail -> Product)
    builder.HasOne(od => od.Product)
        .WithMany(p => p.OrderDetails)
        .HasForeignKey(od => od.ProductId)
        .OnDelete(DeleteBehavior.Cascade); // When a Product is deleted, related
                                           // order details are deleted
  }
}
