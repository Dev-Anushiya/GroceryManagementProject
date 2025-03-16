using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GroceryManSystem.Models
{
    public class DashboardViewModel 
    {
        public int TotalUsers { get; set; }
        public int TotalOrders { get; set; }
        public List<ProductViewModel> LowStockProducts { get; set; }
        public List<PendingOrderViewModel> PendingOrders { get; set; }
    }
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
    }

    public class PendingOrderViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }

    public class OrderItemViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}