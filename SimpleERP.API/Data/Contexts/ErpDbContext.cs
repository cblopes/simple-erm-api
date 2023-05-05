using SimpleERP.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleERP.API.Data.Contexts
{
    public class ErpDbContext : DbContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErpDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
