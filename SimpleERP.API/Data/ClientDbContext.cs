using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Entities;

namespace SimpleERP.API.Data
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }

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
        }
    }
}
