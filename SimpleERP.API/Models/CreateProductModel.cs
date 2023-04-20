namespace SimpleERP.API.Models
{
    public class CreateProductModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public double Price { get; set; }
    }
}
