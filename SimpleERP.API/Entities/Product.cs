namespace SimpleERP.API.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; } = 0;
        public double Price { get; set; }
        public bool IsDeleted { get; set; } = false;

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
