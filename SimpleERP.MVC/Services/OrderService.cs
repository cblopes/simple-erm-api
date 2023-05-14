using Microsoft.Extensions.Options;
using SimpleERP.MVC.Extensions;
using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Services
{
    public class OrderService : Service, IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        private readonly IUser _user;

        public OrderService(HttpClient httpClient, IOptions<AppSettings> settings, IUser user)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _user = user;

            _httpClient.BaseAddress = new Uri(_settings.ApiUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _user.GetUserToken());
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/orders");

            if (response.IsSuccessStatusCode)
            {
                var orders = await DeserializeObjectResponse<IEnumerable<OrderViewModel>>(response);
                return orders;
            }

            return Enumerable.Empty<OrderViewModel>();
        }

        public async Task<EditOrder> GetOrderByIdAsync(Guid? id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/orders/{id}");

            if (response.IsSuccessStatusCode)
            {
                var order = await DeserializeObjectResponse<EditOrder>(response);
                return order;
            }

            return new EditOrder();
        }
    }
}
