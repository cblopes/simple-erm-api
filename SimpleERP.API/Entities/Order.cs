using SimpleERP.API.Entities.Enums;

namespace SimpleERP.API.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; } 
        public DateTime CreatedIn { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Open;
        public DateTime UpdatedIn { get; set;} = DateTime.Now;
        public double Value { get; set; } = 0;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public void FinishOrder()
        {
            OrderStatus = OrderStatus.Finished;
        }

        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }

        public void UpdateTime()
        {
            UpdatedIn = DateTime.Now;
        }

        public void TotalValue()
        {
            double sum = 0;

            foreach (var item in Items)
            {
                sum += item.Amount;
            }

            Value = sum;
        }
    }
}
