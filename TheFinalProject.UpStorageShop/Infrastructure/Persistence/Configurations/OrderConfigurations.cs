using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Identity;
using System.Reflection.Emit;

namespace Infrastructure.Persistance.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();
            // ProductCrawlType
            builder.Property(x => x.ProductCrawlType).IsRequired();

            builder.HasMany(x => x.Product)
              .WithOne(x => x.Order)
              .HasForeignKey(x => x.OrderId);

            builder.HasMany(x => x.OrderEvent)
              .WithOne(x => x.Order)
              .HasForeignKey(x => x.OrderId);

            builder.ToTable("Orders");
        }
    }
}

