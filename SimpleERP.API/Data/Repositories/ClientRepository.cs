using Microsoft.EntityFrameworkCore;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ErpDbContext _context;

        public ClientRepository(ErpDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.Where(c => c.IsActive).ToListAsync();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task CreateAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
