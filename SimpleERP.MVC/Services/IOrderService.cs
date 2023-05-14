using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
        Task<EditOrder> GetOrderByIdAsync(Guid? id);
    }
}
