using SimpleERP.API.Entities;

namespace SimpleERP.API.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(Guid id);
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
    }
}
