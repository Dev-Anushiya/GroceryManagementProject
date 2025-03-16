using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryManSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int PackedBy { get; set; }
        public int OrderBy { get; set; }    
        public int DeliveredBy { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }

        public DateTime? DeliveredDate { get; set; }
        public string ShippingAddress {  get; set; }
        public string ShippingCity {  get; set; }

        public List<OrderItem> Items { get; set; }
    }
}