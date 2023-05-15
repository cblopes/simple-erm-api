using SimpleERP.API.Entities;

namespace SimpleERP.API.Interfaces
{
    public interface IClientServices
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(Guid id);
        Task<Client> GetClientByDocumentAsync(string document);
        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Guid id, Client client);
        Task RemoveClientAsync(Guid id);
    }
}
