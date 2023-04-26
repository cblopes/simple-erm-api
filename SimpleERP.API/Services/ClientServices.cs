using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;
using SimpleERP.API.Models;

namespace SimpleERP.API.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IClientRepository _clientRepository;

        public ClientServices(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task CreateClientAsync(Client client)
        {
            await _clientRepository.CreateAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        public async Task RemoveClientAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client != null)
            {
                await _clientRepository.RemoveAsync(client);
            }
        }
    }
}
