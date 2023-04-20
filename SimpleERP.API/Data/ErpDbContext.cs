using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Entities;

namespace SimpleERP.API.Data
{
    public class ErpDbContext : DbContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>(entity =>
            {
                entity.HasKey(client => client.Id);

                entity.Property(client => client.CpfCnpj)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnType("varchar(14)");

                entity.Property(client => client.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(product => product.Id);

                entity.Property(product => product.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("varchar(10)");

                entity.Property(product => product.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                entity.Property(product => product.QuantityInStock)
                    .HasColumnType("integer");

                entity.Property(product => product.Price)
                    .HasColumnType("decimal(18,2)");
            });
        }
    }
}
