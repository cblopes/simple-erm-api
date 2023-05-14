using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleERP.MVC.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        [DisplayName("Id da Venda")]
        public Guid OrderId { get; set; }

        [DisplayName("Id do produto")]
        public Guid ProductId { get; set; }

        [DisplayName("Quantidade")]
        public int Quantity { get; set; }

        [DisplayName("Valor Unitário")]
        [DataType(DataType.Currency)]
        public double UnitaryValue { get; set; }

        [DisplayName("Total")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
    }
}