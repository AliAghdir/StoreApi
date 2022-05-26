using System;
using System.Collections.Generic;

namespace StoreApi.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public double? Total { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public int SalesPersonId { get; set; }

        public Customer Customer { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
