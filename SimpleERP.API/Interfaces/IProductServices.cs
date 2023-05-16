using SimpleERP.API.Entities;

namespace SimpleERP.API.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> GetProductByCodeAsync(string code);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Guid id, Product product);
        Task RemoveProductAsync(Guid id);
    }
}
