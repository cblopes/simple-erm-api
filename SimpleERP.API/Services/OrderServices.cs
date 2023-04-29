using SimpleERP.API.Entities;
using SimpleERP.API.Entities.Enums;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;

        public OrderServices(IOrderRepository orderRepository, IClientRepository clientRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null) throw new Exception("Pedido não encontrado.");

            return order;
        }

        public async Task CreateOrderAsync(Order order)
        {
            var clientExist = await _clientRepository.GetByIdAsync(order.ClientId);

            if (clientExist == null || !clientExist.IsActive) throw new Exception("Cliente não encontrado.");

            order.Client = clientExist;

            await _orderRepository.CreateAsync(order);
        }

        public async Task FinishOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            
            if (order == null || order.OrderStatus == OrderStatus.Canceled) throw new Exception("Pedido não encontrado ou cancelado");

            if (order.OrderStatus == OrderStatus.Finished) throw new Exception("Pedido já finalizado");

            order.FinishOrder();
            order.UpdateTime();

            await _orderRepository.UpdateAsync(order);
        }

        public async Task CancelOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null || order.OrderStatus == OrderStatus.Canceled) throw new Exception("Pedido não encontrado ou cancelado");

            if (order.OrderStatus == OrderStatus.Finished) throw new Exception("Pedido já finalizado, não pode ser cancelado.");

            order.CancelOrder();
            order.UpdateTime();

            await _orderRepository.UpdateAsync(order);
        }
    }
}
