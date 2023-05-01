using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Services
{
    public interface IAuthService
    {
        Task<ResponseUserLogin> Login(LoginUserViewModel loginUser);
        Task<ResponseUserLogin> Register(RegisterUser registerUser);
    }
}
