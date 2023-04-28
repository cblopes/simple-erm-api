using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;

namespace SimpleERP.API.Services
{
    public class ProductServices : IProductServices
    {
        readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null || product.IsDeleted) throw new ApplicationException("Produto não encontrado.");

            return product;
        }

        public async Task CreateProductAsync(Product product)
        {
            var productExist = await _productRepository.GetByCodeAsync(product.Code);

            if (productExist != null && !productExist.IsDeleted) throw new ApplicationException("Produto já cadastrado.");

            if (productExist != null && productExist.IsDeleted)
            {
                productExist.Update(product.Description, product.QuantityInStock, product.Price);
                productExist.IsDeleted = false;

                await _productRepository.UpdateAsync(productExist);
            }
            else
            {
                await _productRepository.CreateAsync(product);
            }
        }

        public async Task UpdateProductAsync(Guid id, Product product)
        {
            var productExist = await _productRepository.GetByIdAsync(id);

            if (productExist == null || productExist.IsDeleted) throw new ApplicationException("Produto não encontrado.");

            productExist.Update(product.Description, product.QuantityInStock, product.Price);

            await _productRepository.UpdateAsync(productExist);
        }

        public async Task RemoveProductAsync(Guid id)
        {
            var productExist = await _productRepository.GetByIdAsync(id);

            if (productExist == null || productExist.IsDeleted) throw new ApplicationException("Produto não encontrado.");

            productExist.Delete();

            await _productRepository.RemoveAsync(productExist);
        }
    }
}
