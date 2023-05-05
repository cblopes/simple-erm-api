using SimpleERP.API.Entities;
using SimpleERP.API.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleERP.API.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);

            builder.Property(order => order.CreatedIn)
                .HasColumnType("datetime");

            builder.Property(order => order.OrderStatus)
                .HasConversion(
                    s => (char)s,
                    s => (OrderStatus)s)
                .HasColumnType("varchar(1)");

            builder.Property(order => order.UpdatedIn)
                .HasColumnType("datetime");

            builder.Property(order => order.Value)
                .HasColumnType("decimal(18,2)");

            builder.HasOne<Client>()
                .WithMany()
                .HasForeignKey(order => order.ClientId);

            builder.HasMany(order => order.Items)
                .WithOne()
                .HasForeignKey(item => item.OrderId);

            builder.ToTable("Orders");
        }
    }
}
