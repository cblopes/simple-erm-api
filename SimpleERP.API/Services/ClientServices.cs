using SimpleERP.API.Entities;
using SimpleERP.API.Entities.Enums;
using SimpleERP.API.Interfaces;
using System.Reflection.Metadata;

namespace SimpleERP.API.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;

        public ClientServices(IClientRepository clientRepository, IOrderRepository orderRepository)
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            
            if (client == null || !client.IsActive) throw new Exception("Cliente não encontrado.");

            return client;
        }

        public async Task<Client> GetClientByDocumentAsync(string document)
        {
            var client = await _clientRepository.GetByCpfCnpjAsync(document);

            if (client == null || !client.IsActive) throw new Exception("Cliente não encontrado.");

            return client;
        }

        public async Task CreateClientAsync(Client input)
        {
            var document = input.CpfCnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            var client = await _clientRepository.GetByCpfCnpjAsync(document);

            if (client != null && client.IsActive) throw new Exception("Cliente já cadastrado.");

            if (client != null && !client.IsActive)
            {
                client.Update(input.Name);
                client.Active();

                await _clientRepository.UpdateAsync(client);
            }
            else
            {
                await _clientRepository.CreateAsync(input);
            }
        }

        public async Task UpdateClientAsync(Guid id, Client input)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null || !client.IsActive) throw new Exception("Cliente não encontrado.");

            client.Update(input.Name);

            await _clientRepository.UpdateAsync(client);
        }

        public async Task RemoveClientAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null || !client.IsActive) throw new Exception("Cliente não encontrado.");

            var orders = await _orderRepository.GetAllAsync();

            foreach (var order in orders)
            {
                if (order.OrderStatus == OrderStatus.Canceled) continue;

                if (order.ClientId == client.Id) throw new Exception("Cliente possui um pedido em aberto ou finalizado. Não pode ser excluído.");
            }

            client.Delete();

            await _clientRepository.RemoveAsync(client);
        }
    }
}
