using SimpleERP.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleERP.API.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);

            builder.Property(product => product.Code)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(product => product.Description)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(product => product.QuantityInStock)
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(product => product.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(product => product.IsDeleted)
                .IsRequired()
                .HasColumnType("bit");

            builder.ToTable("Products");
        }
    }
}
