using GroceryManSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GroceryManSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly GroceryDbContext _db;

        public OrderService(GroceryDbContext db)
        {
            _db = db;
        }

        public async Task PlaceOrderAsync(Order order, List<OrderItem> cartItems, decimal totalAmount)
        {
            order.Status = "Pending";
            order.CreatedAt = DateTime.Now;
            order.TotalAmount = totalAmount;
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                _db.OrderItems.Add(orderItem);
            }

            await _db.SaveChangesAsync();
        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _db.Orders.Where(o => o.UserId == userId).OrderByDescending(o => o.CreatedAt).ToListAsync();
        }
    }

}