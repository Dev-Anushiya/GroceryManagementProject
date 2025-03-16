using GroceryManSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GroceryManSystem.Services
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(List<OrderItem> cartItems, decimal totalAmount, int orderId);
    }
}