using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleERP.MVC.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Id do Cliente")]
        public Guid ClientId { get; set; }

        [DisplayName("Criado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - HH:mm}")]
        public DateTime CreatedIn { get; set; }

        [DisplayName("Status")]
        public char OrderStatus { get; set; }

        [DisplayName("Atualizado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - HH:mm}")]
        public DateTime UpdatedIn { get; set; }

        [DisplayName("Valor")]
        [DataType(DataType.Currency)]
        public double Value { get; set; }
    }

    public class EditOrder
    {
        public Guid Id { get; set; }

        [DisplayName("Id do Cliente")]
        public Guid ClientId { get; set; }

        [DisplayName("Criado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - HH:mm}")]
        public DateTime CreatedIn { get; set; }

        [DisplayName("Status")]
        public char OrderStatus { get; set; }

        [DisplayName("Atualizado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - HH:mm}")]
        public DateTime UpdatedIn { get; set; }

        [DisplayName("Valor")]
        [DataType(DataType.Currency)]
        public double Value { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
