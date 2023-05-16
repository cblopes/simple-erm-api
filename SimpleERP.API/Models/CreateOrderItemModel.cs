namespace SimpleERP.API.Models
{
    public class CreateOrderItemModel
    {
        public string Code { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
