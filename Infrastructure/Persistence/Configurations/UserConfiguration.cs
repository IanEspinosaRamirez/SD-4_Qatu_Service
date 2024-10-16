using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Users;
using Domain.Entities;
using Domain.Entities.Carts;

namespace Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User> {
  public void Configure(EntityTypeBuilder<User> builder) {
    // Configuración de la tabla
    builder.ToTable("Users");

    // Clave primaria
    builder.HasKey(u => u.Id);
    builder.Property(u => u.Id).HasConversion(id => id.Value,
                                              value => new CustomerId(value));

    // Propiedades requeridas y restricciones de longitud
    builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
    builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
    builder.HasIndex(u => u.Email).IsUnique();

    builder.Property(u => u.Phone).HasMaxLength(15);
    builder.HasIndex(u => u.Phone).IsUnique();

    builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
    builder.HasIndex(u => u.Username).IsUnique();

    builder.Property(u => u.Password).IsRequired();
    builder.Property(u => u.Country).IsRequired().HasMaxLength(50);
    builder.Property(u => u.CreatedAt).IsRequired();
    builder.Property(u => u.UpdatedAt).IsRequired();
    builder.Property(u => u.ActiveAccount).IsRequired();
    builder.Property(u => u.VerifiedAccount).IsRequired();
    builder.Property(u => u.Address).IsRequired().HasMaxLength(200);
    builder.Property(u => u.ImageURL).HasMaxLength(200);

    // Configuración del rol
    builder.Property(u => u.RoleUser).IsRequired().HasConversion<string>();

    // Relaciones

    // Relación uno a muchos con ReviewStore
    builder.HasMany(u => u.ReviewStores)
        .WithOne(rs => rs.User)
        .HasForeignKey(rs => rs.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con ReviewProduct
    builder.HasMany(u => u.ReviewProducts)
        .WithOne(rp => rp.User)
        .HasForeignKey(rp => rp.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con Store
    builder.HasMany(u => u.Stores)
        .WithOne(s => s.User)
        .HasForeignKey(s => s.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a uno con Cart
    builder.HasOne(u => u.Cart)
        .WithOne(c => c.User)
        .HasForeignKey<Cart>(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación uno a muchos con Order
    builder.HasMany(u => u.Orders)
        .WithOne(o => o.User)
        .HasForeignKey(o => o.UserId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
