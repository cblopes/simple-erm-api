using SimpleERP.API.Entities;
using SimpleERP.API.Entities.Enums;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public OrderServices(IOrderRepository orderRepository, IClientRepository clientRepository, 
                             IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
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

            if (order.Value == 0) throw new Exception("O valor do pedido é igual a zero.");

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

            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                product.QuantityInStock += item.Quantity;
                await _productRepository.UpdateAsync(product);

                item.Quantity = 0;
                item.CalculateAmount();
                await _orderItemRepository.UpdateAsync(item);
            }

            order.TotalValue();
            order.CancelOrder();
            order.UpdateTime();

            await _orderRepository.UpdateAsync(order);
        }

        public async Task AddItemAsync(Guid orderId, OrderItem input)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null) throw new Exception("Pedido não encontrado.");

            switch (order.OrderStatus)
            {
                case OrderStatus.Finished: throw new Exception("Pedido já foi finalizado. Inclusão não permitida.");
                case OrderStatus.Canceled: throw new Exception("Pedido já foi cancelado. Inclusão não permitida");
            }

            var product = await _productRepository.GetByIdAsync(input.ProductId);

            if (product == null || product.IsDeleted) throw new Exception("Produto não encontrado");

            var item = await _orderItemRepository.GetByOrderProductIdAsync(orderId, product.Id);

            if (item != null) throw new Exception("O item já foi adicionado anteriormente. Tente alterá-lo.");

            if (input.Quantity < 0) throw new Exception("Informe uma quantidade válida.");

            if (input.Quantity > product.QuantityInStock) throw new Exception("Quantidade informada é superior a quantidade em estoque.");

            input.OrderId = orderId;
            input.UnitaryValue = product.Price;
            input.CalculateAmount();
            await _orderItemRepository.CreateAsync(input);

            product.QuantityInStock -= input.Quantity;
            await _productRepository.UpdateAsync(product);

            order.TotalValue();
            order.UpdateTime();
            await _orderRepository.UpdateAsync(order);
        }

        public async Task AlterQuantityItemAsync(Guid orderId, Guid itemId, OrderItem input)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null) throw new Exception("Pedido não encontrado.");

            switch (order.OrderStatus)
            {
                case OrderStatus.Finished: throw new Exception("Pedido já foi finalizado. Alteração não permitida.");
                case OrderStatus.Canceled: throw new Exception("Pedido já foi cancelado. Alteração não permitida");
            }

            var item = await _orderItemRepository.GetByIdAsync(itemId);

            if (item == null) throw new Exception("Item não encontrado no pedido informado.");

            if (input.Quantity < 0) throw new Exception("Informe uma quantidade válida..");

            int newQuantity = input.Quantity;

            if (newQuantity == item.Quantity ) throw new Exception("Quantidade informada é igual a quantidade do item.");

            var product = await _productRepository.GetByIdAsync(item.ProductId);

            int difference;

            if (newQuantity > item.Quantity)
            {
                difference = newQuantity - item.Quantity;

                if (difference > product.QuantityInStock) throw new Exception("Quantidade informada é superior a quantidade em estoque.");

                product.QuantityInStock -= difference;
                item.Quantity += difference;
            }
            else
            {
                difference = item.Quantity - newQuantity;

                product.QuantityInStock += difference;
                item.Quantity -= difference;
            }

            await _productRepository.UpdateAsync(product);

            item.CalculateAmount();
            await _orderItemRepository.UpdateAsync(item);

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

            var item = await _orderItemRepository.GetByIdAsync(itemId);

            if (item == null) throw new Exception("Item não encontrado no pedido informado.");

            var product = await _productRepository.GetByIdAsync(item.ProductId);

            product.QuantityInStock += item.Quantity;
            await _productRepository.UpdateAsync(product);

            await _orderItemRepository.DeleteAsync(item);

            order.TotalValue();
            order.UpdateTime();
            await _orderRepository.UpdateAsync(order);
        }
    }
}
