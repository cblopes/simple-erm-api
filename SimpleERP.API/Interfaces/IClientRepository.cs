using SimpleERP.API.Entities;

namespace SimpleERP.API.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(Guid id);
        Task<Client> GetByCpfCnpjAsync(string document);
        Task CreateAsync(Client client);
        Task UpdateAsync(Client client);
        Task RemoveAsync(Client client);
    }
}
