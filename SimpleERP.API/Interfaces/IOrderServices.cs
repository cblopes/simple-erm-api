using SimpleERP.API.Entities;

namespace SimpleERP.API.Interfaces
{
    public interface IOrderServices
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        Task CreateOrderAsync(Order order);
        Task FinishOrderAsync(Guid id);
        Task CancelOrderAsync(Guid id);
    }
}
