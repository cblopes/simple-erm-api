using SimpleERP.API.Entities.Enums;
using SimpleERP.API.Entities;

namespace SimpleERP.API.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CreatedIn { get; set; }
        public char OrderStatus { get; set; }
        public DateTime UpdatedIn { get; set; }
        public double Value { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
