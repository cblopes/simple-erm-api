using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        public Task<ProductViewModel> GetProductByIdAsync(Guid? id);
        public Task<ProductViewModel> GetProductByCodeAsync(string? code);
        public Task<CreateProductModel> CreateProductAsync(CreateProductModel model);
        public Task<AlterProductModel> AlterProductAsync(Guid id, AlterProductModel model);
        public Task<DeleteProductModel> DeleteProductAsync(Guid id);
    }
}
