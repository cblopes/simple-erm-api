using SimpleERP.API.Entities;

namespace SimpleERP.API.Services
{
    public interface IClientServices
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(Guid id);
        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task RemoveClientAsync(Guid id);
    }
}
