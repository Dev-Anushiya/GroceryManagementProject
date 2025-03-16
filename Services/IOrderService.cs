using GroceryManSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GroceryManSystem.Services
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(Order order, List<OrderItem> cartItems, decimal totalAmount);
        Task<List<Order>> GetUserOrdersAsync(int userId);
    }
}