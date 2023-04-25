namespace SimpleERP.API.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitaryValue { get; set; }
        public decimal Amount { get; set; }
    }
}