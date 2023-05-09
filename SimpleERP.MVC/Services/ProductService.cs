using Microsoft.Extensions.Options;
using SimpleERP.MVC.Extensions;
using SimpleERP.MVC.Models;
using System.Net;
using System.Security.Authentication;

namespace SimpleERP.MVC.Services
{
    public class ProductService : Service, IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        private readonly IUser _user;

        public ProductService(HttpClient httpClient, IOptions<AppSettings> settings, IUser user)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _user = user;

            _httpClient.BaseAddress = new Uri(_settings.ApiUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _user.GetUserToken());
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/products");

            if (response.IsSuccessStatusCode)
            {
                var products = await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
                return products;
            }

            return Enumerable.Empty<ProductViewModel>();
        }

        public async Task<ProductViewModel> GetProductByIdAsync(Guid? id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/products/{id}");

            if (response.IsSuccessStatusCode)
            {
                var product = await DeserializeObjectResponse<ProductViewModel>(response);
                return product;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AuthenticationException("JWT token inválido");
            }

            throw new HttpRequestException($"Falha ao tentar obter o ID {id}. Código de resposta: {response.StatusCode}");
        }

        public async Task<CreateProductModel> CreateProductAsync(CreateProductModel model)
        {
            var modelContent = GetContent(model);

            var response = await _httpClient.PostAsync("/api/v1/products", modelContent);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return await DeserializeObjectResponse<CreateProductModel>(response);
            }
            else
            {
                var errorResponse = await DeserializeObjectResponse<ResponseResult>(response);
                return new CreateProductModel
                {
                    ResponseResult = errorResponse
                };
            }
        }
    }
}
