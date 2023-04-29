using SimpleERP.API.Entities.Enums;

namespace SimpleERP.API.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CreatedIn { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime UpdatedIn { get; set;}
        public double Value { get; set; }
        public List<OrderItem> Items { get; set; }
        public Client Client { get; set; }

        public Order(Guid clientId) 
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            CreatedIn = DateTime.Now;
            OrderStatus = OrderStatus.Open;
            UpdatedIn = DateTime.Now;
            Value = 0;
            Items = new List<OrderItem>();
        }

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
