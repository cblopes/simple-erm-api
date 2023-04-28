﻿using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Entities;

namespace SimpleERP.API.Data
{
    public class ErpDbContext : DbContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

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

            builder.Entity<Order>(entity =>
            {
                entity.HasKey(order => order.Id);

                entity.HasOne<Client>()
                    .WithOne();

                entity.Property(order => order.CreatedIn)
                    .IsRequired()
                    .HasColumnType("Datetime");

                entity.Property(order => order.OrderStatus)
                    .IsRequired()
                    .HasColumnType("char");

                entity.Property(order => order.UpdatedIn)
                    .IsRequired()
                    .HasColumnType("Datetime");

                entity.Property(order => order.Value)
                    .HasColumnType("decimal(18,2)");

                entity.HasMany(order => order.Items)
                    .WithOne()
                    .HasForeignKey(item => item.OrderId);
            });

            builder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(item => item.Id);

                /*entity.HasOne<Order>()
                    .WithMany();*/

                entity.HasOne<Product>()
                    .WithMany();

                entity.Property(item => item.Quantity)
                    .HasColumnType("integer");

                entity.Property(item => item.UnitaryValue)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(item => item.Amount)
                    .HasColumnType("decimal(18,2)");
            });
        }
    }
}
