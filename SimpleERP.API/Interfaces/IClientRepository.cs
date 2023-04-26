using SimpleERP.API.Entities;
using SimpleERP.API.Models;

namespace SimpleERP.API.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(Guid id);
        Task CreateAsync(Client client);
        Task UpdateAsync(Client client);
        Task RemoveAsync(Client client);
    }
}
