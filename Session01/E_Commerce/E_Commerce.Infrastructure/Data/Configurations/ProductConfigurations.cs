using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Data.Configurations;

internal class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    // Product 1:N ProductBrand
    // Product 1:N ProductType
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.ProductBrand)
               .WithMany()
               .HasForeignKey(p => p.BrandId);

        builder.HasOne(p => p.ProductType)
               .WithMany()
               .HasForeignKey(p => p.TypeId);

        builder.Property(p=> p.Price)
               .HasPrecision(18, 2);

        builder.Property(p => p.Name)
               .HasMaxLength(100);

        builder.Property(p => p.Description)
               .HasMaxLength(500);

        builder.Property(p => p.PictureUrl)
               .HasMaxLength(200);
    }
}