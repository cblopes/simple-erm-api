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
        Task AddItemAsync(Guid orderId, OrderItem item);
        Task AlterQuantityItemAsync(Guid orderId, Guid itemId, OrderItem item);
        Task DeleteItemAsync(Guid orderId, Guid itemId);
    }
}
