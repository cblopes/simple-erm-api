namespace SimpleERP.API.Models
{
    public class CreateOrderItemViewModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
