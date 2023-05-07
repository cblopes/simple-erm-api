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

        public async Task<IEnumerable<ClientViewModel>> GetClients()
        {
            var response = await _httpClient.GetAsync("/api/v1/clients");

            if (response.IsSuccessStatusCode)
            {
                var clients = await DeserializeObjectResponse<IEnumerable<ClientViewModel>>(response);
                return clients;
            }
            
            return Enumerable.Empty<ClientViewModel>();
        }

        public async Task<CreateClientModel> CreateClient(CreateClientModel model)
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
    }
}
