using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleERP.API.Entities;

namespace SimpleERP.API.Data.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(item => item.Id);

            builder.Property(item => item.Quantity)
                .HasColumnType("integer");

            builder.Property(item => item.UnitaryValue)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(item => item.Amount)
                .HasColumnType("decimal(18,2)");

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(item => item.ProductId);

            builder.ToTable("OrderItems");
        }
    }
}
