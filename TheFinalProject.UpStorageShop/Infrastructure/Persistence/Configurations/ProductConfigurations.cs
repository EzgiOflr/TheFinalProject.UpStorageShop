using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // ID
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Name
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(150);

            // price
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Price).HasMaxLength(100);

            // SalePrice
            builder.Property(x => x.SalePrice).IsRequired();
            builder.Property(x => x.SalePrice).HasMaxLength(100);

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();


            builder.Property(x => x.OrderId).IsRequired();


            builder.ToTable("Products");
        }
    }
}
