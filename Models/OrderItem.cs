using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryManSystem.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        // Foreign key reference to the Order table
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        // Foreign key reference to the Product table
        public int ProductId { get; set; }
        public virtual Inventory Product { get; set; }

        // Quantity of the product ordered
        public int Quantity { get; set; }

        // Price per unit at the time of purchase
        public decimal Price { get; set; }

        // Total price for this line item
        public decimal Total => Quantity * Price;
    }
}