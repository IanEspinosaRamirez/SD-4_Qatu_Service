using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.OrderDetails;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail> {
  public void Configure(EntityTypeBuilder<OrderDetail> builder) {
    // Configuración de la tabla
    builder.ToTable("OrderDetails");

    // Clave primaria
    builder.HasKey(od => od.Id);
    builder.Property(od => od.Id)
        .HasConversion(id => id.Value, value => new CustomerId(value));

    builder.Property(od => od.Quantity).IsRequired();
    builder.Property(od => od.UnitPrice).IsRequired();

    // Relaciones

    // Relación con Order
    builder.HasOne(od => od.Order)
        .WithMany(o => o.OrderDetails)
        .HasForeignKey(od => od.OrderId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación con Product
    builder.HasOne(od => od.Product)
        .WithMany(p => p.OrderDetails)
        .HasForeignKey(od => od.ProductId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
