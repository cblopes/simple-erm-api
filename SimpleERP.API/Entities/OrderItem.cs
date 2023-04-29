namespace SimpleERP.API.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        public double UnitaryValue { get; set; }
        public double Amount { get; set; }
    }
}