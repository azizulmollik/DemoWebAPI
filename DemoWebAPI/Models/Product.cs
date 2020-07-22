using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
