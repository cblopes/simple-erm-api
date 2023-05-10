using SimpleERP.MVC.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SimpleERP.MVC.Models
{
    public class ClientViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo {0} deve conter no mínimo {2} e no máximo {1} digitos.")]
        [Display(Name = "CPF/CNPJ")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve conter no mínimo {2} e no máximo {1} caracteres.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }

    public class CreateClientModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo {0} deve conter no mínimo {2} e no máximo {1} digitos.")]
        [Display(Name = "CPF/CNPJ")]
        [CpfValidation(ErrorMessage = "CPF inválido")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve conter no mínimo {2} e no máximo {1} caracteres.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class AlterClientModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve conter no mínimo {2} e no máximo {1} caracteres.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class DeleteClientModel
    {
        public Guid Id { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}
