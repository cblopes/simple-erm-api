using SimpleERP.API.Entities;
using SimpleERP.API.Entities.Enums;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;

        public OrderServices(IOrderRepository orderRepository, IClientRepository clientRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
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
            var client = await _clientRepository.GetByIdAsync(order.ClientId);

            if (client == null || !client.IsActive) throw new Exception("Cliente não encontrado.");

            await _orderRepository.CreateAsync(order);
        }

        public async Task FinishOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            
            if (order == null) throw new Exception("Pedido não encontrado ou cancelado");

            switch (order.OrderStatus)
            {
                case OrderStatus.Finished: throw new Exception("Pedido já foi finalizado.");
                case OrderStatus.Canceled: throw new Exception("Pedido cancelado.");
            }

            order.FinishOrder();
            order.UpdateTime();

            await _orderRepository.UpdateAsync(order);
        }

        public async Task CancelOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null) throw new Exception("Pedido não encontrado");

            switch (order.OrderStatus)
            {
                case OrderStatus.Finished: throw new Exception("Pedido finalizado, não pode ser cancelado");
                case OrderStatus.Canceled: throw new Exception("Pedido já foi cancelado.");
            }

            order.CancelOrder();
            order.UpdateTime();

            await _orderRepository.UpdateAsync(order);
        }

        public async Task AddItemAsync(Guid orderId, OrderItem item)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null) throw new Exception("Pedido não encontrado.");

            switch (order.OrderStatus)
            {
                case OrderStatus.Finished: throw new Exception("Pedido já foi finalizado. Inclusão não permitida.");
                case OrderStatus.Canceled: throw new Exception("Pedido cancelado.");
            }

            var product = await _productRepository.GetByIdAsync(item.ProductId);

            if (product == null || product.IsDeleted) throw new Exception("Produto não encontrado");

            var itemExist = await _orderItemRepository.GetByOrderProductIdAsync(orderId, product.Id);

            if (itemExist != null) throw new Exception("O item já foi adicionado anteriormente. Tente alterá-lo.");

            if (item.Quantity > product.QuantityInStock) throw new Exception("Quantidade informada é superior a quantidade em estoque.");

            item.OrderId = orderId;
            item.UnitaryValue = product.Price;
            item.CalculateAmount();
            await _orderItemRepository.CreateAsync(item);

            product.QuantityInStock -= item.Quantity;
            await _productRepository.UpdateAsync(product);

            order.TotalValue();
            order.UpdateTime();
            await _orderRepository.UpdateAsync(order);
        }

        public async Task AlterQuantityItemAsync(Guid orderId, Guid itemId, OrderItem item)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null) throw new Exception("Pedido não encontrado.");

            switch (order.OrderStatus)
            {
                case OrderStatus.Finished: throw new Exception("Pedido já foi finalizado. Alteração não permitida.");
                case OrderStatus.Canceled: throw new Exception("Pedido cancelado.");
            }

            var itemExist = await _orderItemRepository.GetByIdAsync(itemId);

            if (itemExist == null) throw new Exception("Item não encontrado no pedido informado.");

            int newQuantity = item.Quantity;

            if (newQuantity == itemExist.Quantity ) throw new Exception("Quantidade informada é igual a quantidade do item.");

            var product = await _productRepository.GetByIdAsync(itemExist.ProductId);

            int difference;

            if (newQuantity > itemExist.Quantity)
            {
                difference = newQuantity - itemExist.Quantity;

                if (difference > product.QuantityInStock) throw new Exception("Quantidade informada é superior a quantidade em estoque.");

                product.QuantityInStock -= difference;
                itemExist.Quantity += difference;
            }
            else
            {
                difference = itemExist.Quantity - newQuantity;

                product.QuantityInStock += difference;
                itemExist.Quantity -= difference;
            }

            await _productRepository.UpdateAsync(product);

            itemExist.CalculateAmount();
            await _orderItemRepository.UpdateAsync(itemExist);

            order.TotalValue();
            order.UpdateTime();
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteItemAsync(Guid orderId, Guid itemId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null) throw new Exception("Pedido não encontrado.");

            switch (order.OrderStatus)
            {
                case OrderStatus.Finished: throw new Exception("Pedido já foi finalizado. Remoção não permitida.");
                case OrderStatus.Canceled: throw new Exception("Pedido já foi cancelado. Remoção não permitida.");
            }

            var itemExist = await _orderItemRepository.GetByIdAsync(itemId);

            if (itemExist == null) throw new Exception("Item não encontrado no pedido informado.");

            var product = await _productRepository.GetByIdAsync(itemExist.ProductId);

            product.QuantityInStock += itemExist.Quantity;
            await _productRepository.UpdateAsync(product);

            await _orderItemRepository.DeleteAsync(itemExist);

            order.TotalValue();
            order.UpdateTime();
            await _orderRepository.UpdateAsync(order);
        }
    }
}
