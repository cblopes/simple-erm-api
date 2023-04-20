namespace SimpleERP.API.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public double Price { get; set; }
    }
}
