using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.ReviewStores;
using Domain.Entities.Users;
using Domain.Entities.Stores;

namespace Infrastructure.Persistence.Configuration;

public class ReviewStoreConfiguration : IEntityTypeConfiguration<ReviewStore> {
  public void Configure(EntityTypeBuilder<ReviewStore> builder) {
    // Table configuration
    builder.ToTable("ReviewStores");

    // Primary key configuration
    builder.HasKey(rs => rs.Id);

    // Required properties and length restrictions
    builder.Property(rs => rs.Rating).IsRequired();

    builder.Property(rs => rs.Content).IsRequired().HasMaxLength(1000);

    builder.Property(rs => rs.CreatedAt).IsRequired();

    // Relationship configurations

    // Foreign key for User (ReviewStore -> User)
    builder
        .HasOne<User>() // No need to reference the User property
        .WithMany(u => u.ReviewStores)
        .HasForeignKey(rs => rs.UserId)
        .OnDelete(DeleteBehavior.Cascade); // When a User is deleted, the
                                           // reviews are deleted as well

    // Foreign key for Store (ReviewStore -> Store)
    builder
        .HasOne<Store>() // No need to reference the Store property
        .WithMany(s => s.ReviewStores)
        .HasForeignKey(rs => rs.StoreId)
        .OnDelete(DeleteBehavior.Cascade); // When a Store is deleted, the
                                           // reviews are deleted as well
  }
}
