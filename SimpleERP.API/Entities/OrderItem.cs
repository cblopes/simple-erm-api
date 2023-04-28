namespace SimpleERP.API.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitaryValue { get; set; }
        public double Amount { get; set; }
    }
}