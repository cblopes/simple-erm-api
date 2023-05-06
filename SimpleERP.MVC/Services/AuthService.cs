using Microsoft.AspNetCore.Authentication.Cookies;
using SimpleERP.MVC.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Options;
using SimpleERP.MVC.Extensions;

namespace SimpleERP.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;

            _httpClient.BaseAddress = new Uri(_settings.ApiUrl);
        }

        public async Task<ResponseUserLogin> Login(LoginUser loginUser)
        {
            var loginContent = GetContent(loginUser);

            var response = await _httpClient.PostAsync("/api/v1/accounts/login", loginContent);

            if (!HandleResponseErros(response))
            {
                return new ResponseUserLogin
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<ResponseUserLogin>(response);
        }

        public async Task<ResponseUserLogin> Register(RegisterUser registerUser)
        {
            var registerContent = GetContent(registerUser);

            var response = await _httpClient.PostAsync("/api/v1/accounts/register", registerContent);

            if (!HandleResponseErros(response))
            {
                return new ResponseUserLogin
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<ResponseUserLogin>(response);
        }
    }
}
