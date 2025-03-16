using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GroceryManSystem.Models;
using PayPal;
using PayPal.Api;

namespace GroceryManSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaypalService _paypalService;
        public async Task<string> ProcessPaymentAsync(List<OrderItem> cartItems, decimal totalAmount, int orderId)
        {
            // Simulate PayPal payment process (or replace with actual logic)
            await Task.Delay(500);  // Simulating async payment processing
            return "Payment Processed";
        }
        public PaymentService()
        {
            var paypalService = PaypalService.Instance;
            string token = paypalService.GetAccessTokenAsync().Result;
            //_paypalService = paypalService ?? throw new ArgumentNullException(nameof(paypalService));
        }


        public async Task<string> ProcessPayment(List<OrderItem> cartItems, decimal totalAmount, int orderId)
        {
            try
            {
                string accessToken = await _paypalService.GetAccessTokenAsync();
                var apiContext = new APIContext(accessToken);

                // Use the factory method to create the Payment object
                var payment = CreatePayment(cartItems, totalAmount, orderId);

                // Execute payment
                var createdPayment = payment.Create(apiContext);

                // Retrieve PayPal approval URL and redirect user
                var approvalUrl = createdPayment.links.FirstOrDefault(link => link.rel == "approval_url")?.href;
                return approvalUrl ?? "Payment failed";
            }
            catch (PayPalException ex)
            {
                Console.WriteLine($"PayPal error: {ex.Message}");
                return "0";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                return "0";
            }
        }

        // Factory Method for creating Payment objects
        protected virtual Payment CreatePayment(List<OrderItem> cartItems, decimal totalAmount, int orderId)
        {
            var itemList = new ItemList
            {
                items = cartItems.Select(item => new Item
                {
                    currency = "USD",
                    price = item.Price.ToString("F2"),
                    quantity = item.Quantity.ToString(),
                    sku = item.ProductId.ToString() // Optional SKU
                }).ToList()
            };

            return new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
            {
                new Transaction
                {
                    amount = new Amount
                    {
                        currency = "USD",
                        total = totalAmount.ToString("F2")
                    },
                    item_list = itemList,
                    description = $"Order #{orderId}"
                }
            },
                redirect_urls = new RedirectUrls
                {
                    return_url = "https://localhost:44384/Inventory/Index",
                    cancel_url = "https://localhost:44384/Cart/Cart"
                }
            };
        }
    }
}