using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Data.Contexts;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ErpDbContext _context;

        public ProductRepository( ErpDbContext context )
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Where(p => !p.IsDeleted).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetByCodeAsync(string code)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.Code == code && !p.IsDeleted);
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
