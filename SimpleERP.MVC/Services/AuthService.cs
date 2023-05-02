using Microsoft.AspNetCore.Authentication.Cookies;
using SimpleERP.MVC.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Net.Http;

namespace SimpleERP.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseUserLogin> Login(LoginUser loginUser)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(loginUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7182/api/v1/accounts/login", loginContent);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!HandleResponseErros(response))
            {
                return new ResponseUserLogin
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<ResponseUserLogin>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<ResponseUserLogin> Register(RegisterUser registerUser)
        {
            var registerContent = new StringContent(
                JsonSerializer.Serialize(registerUser),
                Encoding.UTF8,
                "application;/json");

            var response = await _httpClient.PostAsync("https://localhost:7182/api/v1/accounts/register", registerContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!HandleResponseErros(response))
            {
                return new ResponseUserLogin
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<ResponseUserLogin>(await response.Content.ReadAsStringAsync());
        }
    }
}
