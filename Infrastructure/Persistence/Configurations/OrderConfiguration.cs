using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Orders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order> {
  public void Configure(EntityTypeBuilder<Order> builder) {
    // Configuración de la tabla
    builder.ToTable("Orders");

    // Clave primaria
    builder.HasKey(o => o.Id);
    builder.Property(o => o.Id).HasConversion(id => id.Value,
                                              value => new CustomerId(value));

    builder.Property(o => o.TotalPrice).IsRequired();
    builder.Property(o => o.ShippingMethod).IsRequired().HasMaxLength(50);
    builder.Property(o => o.PaymentMethod).IsRequired().HasMaxLength(50);
    builder.Property(o => o.OrderDate).IsRequired();

    // Relaciones

    // Relación con User
    builder.HasOne(o => o.User)
        .WithMany(u => u.Orders)
        .HasForeignKey(o => o.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con OrderDetail
    builder.HasMany(o => o.OrderDetails)
        .WithOne(od => od.Order)
        .HasForeignKey(od => od.OrderId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
