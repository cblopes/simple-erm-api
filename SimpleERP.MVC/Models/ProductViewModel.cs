using System.ComponentModel.DataAnnotations;

namespace SimpleERP.MVC.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Código")]
        [StringLength(10, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Quantidade em Estoque")]
        [Range(0, int.MaxValue, ErrorMessage = "O campo {0} não pode ser menor que 0.")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "O campo {0} não pode ser menor que 1.")]
        public double Price { get; set; }
    }

    public class CreateProductModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Código")]
        [StringLength(10, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Quantidade em Estoque")]
        [Range(0, int.MaxValue, ErrorMessage = "O campo {0} não pode ser menor que 0.")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Preço")]
        public double Price { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class AlterProductModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Quantidade em Estoque")]
        [Range(0, int.MaxValue, ErrorMessage = "O campo {0} não pode ser menor que 0.")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "O campo {0} não pode ser menor que 1.")]
        public double Price { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class DeleteProductModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Código")]
        public string Code { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Quantidade em Estoque")]
        public int QuantityInStock { get; set; }

        [Display(Name = "Preço")]
        public double Price { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}
