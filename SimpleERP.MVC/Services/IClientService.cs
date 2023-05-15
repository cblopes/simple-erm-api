using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Services
{
    public interface IClientService
    {
        public Task<IEnumerable<ClientViewModel>> GetClientsAsync();
        public Task<ClientViewModel> GetClientByDocumentAsync(string? document);
        public Task<CreateClientModel> CreateClientAsync(CreateClientModel clientViewModel);
        public Task<AlterClientModel> AlterClientAsync(Guid id, AlterClientModel clientViewModel);
        public Task<DeleteClientModel> DeleteClientAsync(Guid id);
    }
}
