using Microsoft.Extensions.Options;
using SimpleERP.MVC.Extensions;
using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Services
{
    public class ClientService : Service, IClientService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        private readonly IUser _user;

        public ClientService(HttpClient httpClient, IOptions<AppSettings> settings, IUser user)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _user = user;

            _httpClient.BaseAddress = new Uri(_settings.ApiUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _user.GetUserToken());
        }

        public async Task<IEnumerable<ClientViewModel>> GetClientsAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/clients");

            if (response.IsSuccessStatusCode)
            {
                var clients = await DeserializeObjectResponse<IEnumerable<ClientViewModel>>(response);
                return clients;
            }

            return Enumerable.Empty<ClientViewModel>();
        }

        public async Task<ClientViewModel> GetClientByDocumentAsync(string document)
        {
            var response = await _httpClient.GetAsync($"/api/v1/clients/{document}");

            if (response.IsSuccessStatusCode)
            {
                var client = await DeserializeObjectResponse<ClientViewModel>(response);
                return client;
            }

            return new ClientViewModel();
        }

        public async Task<CreateClientModel> CreateClientAsync(CreateClientModel model)
        {
            var modelContent = GetContent(model);

            var response = await _httpClient.PostAsync("/api/v1/clients", modelContent);
            if (!HandleResponseErros(response))
            {
                return new CreateClientModel
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<CreateClientModel>(response);
        }

        public async Task<AlterClientModel> AlterClientAsync(Guid id, AlterClientModel model)
        {
            var modelContent = GetContent(model);

            var response = await _httpClient.PutAsync($"/api/v1/clients/{id}", modelContent);

            if (!HandleResponseErros(response))
            {
                return new AlterClientModel
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<AlterClientModel>(response);
        }

        public async Task<DeleteClientModel> DeleteClientAsync(Guid id)
        {

            var response = await _httpClient.DeleteAsync($"/api/v1/clients/{id}");

            if (!HandleResponseErros(response))
            {
                return new DeleteClientModel
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<DeleteClientModel>(response);
        }
    }
}
