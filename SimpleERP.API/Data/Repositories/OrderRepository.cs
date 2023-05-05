using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Data.Contexts;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ErpDbContext _context;

        public OrderRepository(ErpDbContext context) 
        { 
            _context = context; 
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.AsQueryable().ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
