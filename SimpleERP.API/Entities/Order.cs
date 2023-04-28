using SimpleERP.API.Entities.Enums;

namespace SimpleERP.API.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CreatedIn { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime UpdatedIn { get; set;}
        public double Value { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
