using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        public Task<ProductViewModel> GetProductByIdAsync(Guid? id);
        public Task<CreateProductModel> CreateProductAsync(CreateProductModel model);
    }
}
