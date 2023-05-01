namespace SimpleERP.API.Models
{
    public class CreateOrderItemModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
