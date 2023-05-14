using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleERP.MVC.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(32, ErrorMessage = "O campor {0} deve conter entre {2} e {1} caracteres.", MinimumLength = 8)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        [DisplayName("Confirmar senha")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUser
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(32, ErrorMessage = "O campor {0} deve conter entre {2} e {1} caracteres.")]
        [DisplayName("Senha")]
        public string Password { get; set; }
    }

    public class ResponseUserLogin
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
