using SimpleERP.API.Entities;

namespace SimpleERP.API.Data
{
    public class ClientDbContext
    {
        public List<Client> Clients { get; set; }

        // O context foi adicionado dessa forma para testes sem adição de um banco de dados
        public ClientDbContext()
        {
            Clients = new List<Client>();
        }
    }
}
