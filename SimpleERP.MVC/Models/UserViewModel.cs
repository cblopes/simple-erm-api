using System.ComponentModel.DataAnnotations;

namespace SimpleERP.MVC.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(32, ErrorMessage = "O campor {0} deve conter entre {2} e {1} caracteres.", MinimumLength = 8)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(32, ErrorMessage = "O campor {0} deve conter entre {2} e {1} caracteres.")]
        public string Password { get; set; }
    }

    public class ResponseUserLogin
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}
