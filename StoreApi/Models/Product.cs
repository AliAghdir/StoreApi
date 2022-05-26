using System;
using System.Collections.Generic;

namespace StoreApi.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Size { get; set; }
        public string Varienty { get; set; }
        public double? Price { get; set; }
        public string Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
