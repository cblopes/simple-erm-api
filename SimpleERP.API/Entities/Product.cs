namespace SimpleERP.API.Entities
{
    public class Product
    {
        public Product()
        {
            Description = "";
            QuantityInStock = 0;
            IsDeleted = false;
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }

        public void Update(string description,int quantityInStock, double price)
        {
            Description = description;
            QuantityInStock = quantityInStock;
            Price = price;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
