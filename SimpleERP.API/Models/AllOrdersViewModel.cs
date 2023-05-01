namespace SimpleERP.API.Models
{
    public class AllOrdersViewModel
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CreatedIn { get; set; }
        public char OrderStatus { get; set; }
        public DateTime UpdatedIn { get; set; }
        public double Value { get; set; }
    }
}
