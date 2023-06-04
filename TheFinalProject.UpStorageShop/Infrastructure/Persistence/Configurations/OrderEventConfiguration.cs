using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class OrderEventConfiguration : IEntityTypeConfiguration<OrderEvent>
    {
        public void Configure(EntityTypeBuilder<OrderEvent> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            builder.Property(x => x.OrderId).IsRequired();

            builder.Property(x => x.Status).IsRequired();


            builder.ToTable("OrderEvents");



        }
    }
}
