using GroceryManSystem.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryManSystem.Controllers
{
    public class DeliveryPersonController : Controller
    {
        private readonly GroceryDbContext db = new GroceryDbContext();

        // GET: DeliveryPerson/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: DeliveryPerson/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(DeliveryPerson model)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryPersons1.Add(model);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: DeliveryPerson/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: DeliveryPerson/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var deliveryPerson = db.DeliveryPersons1
                                    .FirstOrDefault(dp => dp.Email == email && dp.Password == password);

            if (deliveryPerson != null)
            {
                Session["DeliveryPersonId"] = deliveryPerson.DeliveryPersonId;
                Session["DeliveryPersonName"] = deliveryPerson.Name;
                return RedirectToAction("PendingOrders"); // Example, redirect to the Dashboard after successful login.
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        // GET: DeliveryPerson/Dashboard
        public ActionResult PendingOrders()
        {
            var orders = db.Orders
                .Where(o => o.Status == "Packed" && o.DeliveredBy == 0)  
                .Select(o => new DeliveryOrderViewModel
                {
                   OrderId = o.OrderId,
                    UserId=o.UserId,
                    Status= o.Status,
                    ShippingAddress= o.ShippingAddress,
                    ShippingCity =o.ShippingCity,
                    TotalAmount = o.TotalAmount,
                    CustomerName = db.Users.FirstOrDefault(u => u.UserId == o.UserId).Username
                })
                .ToList();

            return View(orders);
        }
        // GET: DeliveryPerson
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarkAsDelivered(int orderId)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = "Delivered";
                order.DeliveredBy = Convert.ToInt32(Session["DeliveryPersonId"]); 
                order.DeliveredDate = DateTime.Now;  
                db.SaveChanges();
            }

            return RedirectToAction("PendingOrders");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}