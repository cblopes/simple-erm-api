using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Data.Contexts;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ErpDbContext _context;
        private readonly string _connectionString;

        public OrderItemRepository(ErpDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DataApiCs");
        }

        public async Task<OrderItem> GetByIdAsync(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetByOrderProductIdAsync(Guid orderId, Guid productId)
        {
            var parameters = new { orderId, productId };

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM OrderItems WHERE OrderId = @orderId AND ProductId = @productId";

                var orderItem = await sqlConnection.QuerySingleOrDefaultAsync<OrderItem>(sql, parameters);

                return orderItem;
            }
        }

        public async Task CreateAsync(OrderItem item)
        {
            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderItem item)
        {
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
