using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ErpDbContext _context;

        public OrderItemRepository(ErpDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetByIdAsync(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetByOrderProductIdAsync(Guid orderId, Guid productId)
        {
            return await _context.OrderItems.SingleOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);
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
