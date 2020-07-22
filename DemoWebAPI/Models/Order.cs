using System;
using System.Collections.Generic;

namespace DemoWebAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderedDate { get; set; }
        public int Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
