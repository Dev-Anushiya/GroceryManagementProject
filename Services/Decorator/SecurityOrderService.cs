using GroceryManSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GroceryManSystem.Services.Decorator
{
    public class SecurityOrderService
    {
        private readonly IOrderService _innerOrderService;

        public SecurityOrderService(IOrderService innerOrderService)
        {
            _innerOrderService = innerOrderService;
        }

        public async Task PlaceOrderAsync(Order order, List<OrderItem> cartItems, decimal totalAmount)
        {
            if (totalAmount > 1000)  // Simple security check
            {
                throw new Exception("Amount exceeds limit for placing orders.");
            }

            await _innerOrderService.PlaceOrderAsync(order, cartItems, totalAmount);
        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _innerOrderService.GetUserOrdersAsync(userId);
        }
    }
}