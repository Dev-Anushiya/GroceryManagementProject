using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using GroceryManSystem.
using GroceryManSystem.Commands;
using GroceryManSystem.Services;
using GroceryManSystem.Models;
using System.Threading.Tasks;
//using GroceryManagement1.Services;
//using PayPal.Api;

namespace GroceryManSystem.Controllers
{   

    public class CartController : Controller
    {

        private GroceryDbContext db = new GroceryDbContext();

        private readonly PaymentService _paymentService;
        private readonly CommandInvoker _commandInvoker;

        public CartController()
        {
            //_paymentService = new PaymentService();
            _commandInvoker = new CommandInvoker();
        }
        // GET: Cart
        public ActionResult Cart()
        {
            return View("Cart");
        }
        [HttpGet]
        public ActionResult PlaceOrder()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PlaceOrder(Models.Order order, List<OrderItem> cartItems, decimal totalAmount)
        {
            if (cartItems == null || !cartItems.Any())
            {
                ModelState.AddModelError("", "No items in the cart.");
                return View(order);
            }

            order.UserId = Convert.ToInt32(Session["UserId"]);
            order.OrderBy = Convert.ToInt32(Session["UserId"]);
            order.Status = "Pending";
            order.CreatedAt = DateTime.Now;
            order.TotalAmount = totalAmount;

            // Save order in the database
            db.Orders.Add(order);
            await db.SaveChangesAsync();

            // Process payment with PayPal
            //var paymentService = new PaymentService();
            //string redirectUrl = await paymentService.ProcessPayment(cartItems, totalAmount, order.OrderId);

            //if (redirectUrl != "0")
            //{
                foreach (var item in cartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId, // Use the generated OrderId
                        ProductId = item.ProductId,    // Reference to the product
                        Quantity = item.Quantity,
                        Price = item.Price       // Per-item price
                    };

                    db.OrderItems.Add(orderItem);
                }
                // Redirect to PayPal for approval
                //return Redirect(redirectUrl);
            ////}
            ////else { 
            //// If payment fails, remove the order from the database
            //    db.Orders.Remove(order);

            //}

            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Inventory", new { orderId = order.OrderId });

        }

        public ActionResult MyOrders()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var orders = db.Orders
                           .Where(o => o.UserId == userId)
                           .OrderByDescending(o => o.CreatedAt)
                           .ToList();

            return View(orders);
        }
    }
}