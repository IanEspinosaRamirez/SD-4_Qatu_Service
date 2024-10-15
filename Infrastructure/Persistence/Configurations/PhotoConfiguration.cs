using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Photos;
using Domain.Entities.Products;
using Domain.Entities.Stores;

namespace Infrastructure.Persistence.Configuration;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo> {
  public void Configure(EntityTypeBuilder<Photo> builder) {
    // Table configuration
    builder.ToTable("Photos");

    // Primary key configuration
    builder.HasKey(p => p.Id);

    // Required properties and length restrictions
    builder.Property(p => p.ImageURL).IsRequired().HasMaxLength(500);

    // Optional Foreign Key for Product
    builder
        .HasOne<Product>() // No need to reference the Product navigation
                           // property
        .WithMany(pr => pr.Photos)
        .HasForeignKey(p => p.ProductId)
        .OnDelete(
            DeleteBehavior
                .SetNull); // Set the foreign key to null if Product is deleted

    // Optional Foreign Key for Store
    builder
        .HasOne<Store>() // No need to reference the Store navigation property
        .WithMany(s => s.Photos)
        .HasForeignKey(p => p.StoreId)
        .OnDelete(
            DeleteBehavior
                .SetNull); // Set the foreign key to null if Store is deleted
  }
}
