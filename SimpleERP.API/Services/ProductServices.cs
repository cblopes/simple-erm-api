using SimpleERP.API.Entities;
using SimpleERP.API.Entities.Enums;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductServices(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null || product.IsDeleted) throw new Exception("Produto não encontrado.");

            return product;
        }

        public async Task<Product> GetProductByCodeAsync(string code)
        {
            var product = await _productRepository.GetByCodeAsync(code);

            if (product == null || product.IsDeleted) throw new Exception("Produto não encontrado.");

            return product;
        }

        public async Task CreateProductAsync(Product input)
        {
            var product = await _productRepository.GetByCodeAsync(input.Code);

            if (product != null && !product.IsDeleted) throw new ApplicationException("Produto já cadastrado.");

            await _productRepository.CreateAsync(input);
        }

        public async Task UpdateProductAsync(Guid id, Product input)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null || product.IsDeleted) throw new Exception("Produto não encontrado.");

            product.Update(input.Description, input.QuantityInStock, input.Price);

            await _productRepository.UpdateAsync(product);
        }

        public async Task RemoveProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null || product.IsDeleted) throw new Exception("Produto não encontrado.");

            var orders = await _orderRepository.GetAllAsync();

            foreach (var order in orders)
            {
                if (order.OrderStatus == OrderStatus.Canceled) continue;

                foreach (var item in order.Items)
                {
                    if (item.ProductId == id) throw new Exception("O produto não pode ser removido pois está em pedidos em aberto ou finalizados.");
                }
            }

            if (product.QuantityInStock > 0) throw new Exception("O produto não pode ser removido pois ainda há unidades em estoque.");

            product.Delete();

            await _productRepository.RemoveAsync(product);
        }
    }
}
