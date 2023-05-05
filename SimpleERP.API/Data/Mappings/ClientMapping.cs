using SimpleERP.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleERP.API.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(client => client.Id);

            builder.Property(client => client.CpfCnpj)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(client => client.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(client => client.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder.ToTable("Clients");
        }
    }
}
