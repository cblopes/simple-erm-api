using SimpleERP.API.Entities;

namespace SimpleERP.API.Data
{
    public class ProductDbContext
    {
        public List<Product> Products { get; set; }

        // O context foi adicionado dessa forma para testes sem adição de um banco de dados
        public ProductDbContext()
        {
            Products = new List<Product>();
        }
    }
}
