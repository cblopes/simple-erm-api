using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;

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
            var clientExist = await _clientRepository.GetByCpfCnpjAsync(client.CpfCnpj);

            if (clientExist != null && clientExist.IsActive) throw new ApplicationException("Cliente já cadastrado.");

            if (clientExist != null && !clientExist.IsActive)
            {
                clientExist.Update(client.Name);
                clientExist.Active();

                await _clientRepository.UpdateAsync(clientExist);
            }
            else
            {
                await _clientRepository.CreateAsync(client);
            }
        }

        public async Task UpdateClientAsync(Guid id, Client client)
        {
            var clientExist = await _clientRepository.GetByIdAsync(id);

            if (clientExist == null || !clientExist.IsActive) throw new ApplicationException("Cliente não encontrado.");

            clientExist.Update(client.Name);

            await _clientRepository.UpdateAsync(clientExist);
        }

        public async Task RemoveClientAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null || !client.IsActive) throw new ApplicationException("Cliente não encontrado.");

            client.Delete();

            await _clientRepository.RemoveAsync(client);
        }
    }
}
