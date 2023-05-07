using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Services
{
    public interface IClientService
    {
        public Task<IEnumerable<ClientViewModel>> GetClients();
        public Task<CreateClientModel> CreateClient(CreateClientModel clientViewModel);
    }
}
