using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryManSystem.Models
{
    public class PackingOrderViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public List<PackingOrderItemViewModel> Items { get; set; }
    }
    public class PackingOrderItemViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}