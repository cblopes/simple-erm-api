using Microsoft.AspNetCore.Mvc;
using SimpleERP.MVC.Models;

namespace SimpleERP.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool HasErrorsResponse(ResponseResult response)
        {
            if (response != null && response.Errors.Mensagens.Any())
            {
                return true;
            }

            return false;
        }
    }
}
