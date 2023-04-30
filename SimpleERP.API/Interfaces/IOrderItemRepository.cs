using SimpleERP.API.Entities;

namespace SimpleERP.API.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> GetByIdAsync(Guid id);
        Task<OrderItem> GetByOrderProductIdAsync(Guid orderId, Guid productId);
        Task CreateAsync(OrderItem item);
        Task UpdateAsync(OrderItem item);
        Task DeleteAsync(OrderItem item);
    }
}
