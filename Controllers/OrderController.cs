using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryManSystem.Models;
using GroceryManSystem.Services;
using GroceryManSystem.Commands;

namespace GroceryManagement1.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private GroceryDbContext db = new GroceryDbContext();
        private readonly PaymentService _paymentService;
        private readonly CommandInvoker _commandInvoker;

        public OrderController()
        {
            _paymentService = new PaymentService();
            _commandInvoker = new CommandInvoker();
        }
        //[HttpGet]
        //public ActionResult PlaceOrder()
        //{
        //    var userId = (int)Session["UserId"];
        //    var isPremium = (bool)Session["IsPremium"];
        //    var cart = (Inventory)Session["Cart"];

        //    if (cart == null) return RedirectToAction("Index", "Inventory");

        //    decimal discount = isPremium ? 0.05m : 0;
        //    decimal total = cart.Price - (cart.Price * discount);

        //    var order = new Order
        //    {
        //        UserId = userId,
        //        Status = "Pending",
        //        TotalAmount = total
        //    };

        //    db.Orders.Add(order);
        //    db.SaveChanges();

        //    return View(order);
        //}
        //[HttpPost]
        

        //public ActionResult OrderConfirmation(int orderId)
        //{
        //    var order = /* fetch order by orderId */;
        //    return View(order);
        //}
    }
}