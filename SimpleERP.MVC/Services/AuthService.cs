using Microsoft.AspNetCore.Authentication.Cookies;
using SimpleERP.MVC.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Net.Http;

namespace SimpleERP.MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseUserLogin> Login(LoginUserViewModel loginUser)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(loginUser),
                Encoding.UTF8,
                "application/json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var response = await _httpClient.PostAsync("https://localhost:7182/api/v1/accounts/login", loginContent);

            return JsonSerializer.Deserialize<ResponseUserLogin>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<ResponseUserLogin> Register(RegisterUserViewModel registerUser)
        {
            var registerContent = new StringContent(
                JsonSerializer.Serialize(registerUser),
                Encoding.UTF8,
                "application;/json");

            var response = await _httpClient.PostAsync("https://localhost:7182/api/v1/accounts/register", registerContent);

            return JsonSerializer.Deserialize<ResponseUserLogin>(await response.Content.ReadAsStringAsync());
        }
    }
}
