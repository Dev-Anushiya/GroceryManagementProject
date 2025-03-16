using GroceryManSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryManSystem.Controllers
{
    public class PackingPersonController : Controller

    {
        private readonly GroceryDbContext db = new GroceryDbContext();

        // GET: Register
        // GET: PackingPerson/Register

        public ActionResult Register()
        {
            return View();
        }

        // POST: PackingPerson/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(PackingPerson packingPerson)
        {
            if (ModelState.IsValid)
            {
                // Add to database
                db.PackingPersons.Add(packingPerson);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(packingPerson);
        }

        // GET: PackingPerson/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: PackingPerson/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var person = db.PackingPersons.SingleOrDefault(p => p.Email == email && p.Password == password);
            if (person != null)
            {
                // Redirect to Dashboard or another view
                Session["PackingPersonId"] = person.PackingPersonId;
                Session["PackingPersonName"] = person.Name;
                return RedirectToAction("PendingOrders");
            }
            ViewBag.ErrorMessage = "Invalid Email or Password.";
            return View();
        }

        // GET: PackingPerson/Dashboard
        //public ActionResult Dashboard()
        //{
        //    if (Session["PackingPersonId"] == null)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    return View();
        //}

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult PendingOrders()
        {
            var orders = (from o in db.Orders
                          where o.Status == "Pending"
                          join u in db.Users on o.UserId equals u.UserId
                          join oi in db.OrderItems on o.OrderId equals oi.OrderId
                          join i in db.Inventories on oi.ProductId equals i.ProductId
                          group new { o, u, oi, i } by new
                          {
                              o.OrderId,
                              o.UserId,
                              o.TotalAmount,
                              o.Status,
                              CustomerName = u.Username
                          } into grouped
                          select new PackingOrderViewModel
                          {
                              OrderId = grouped.Key.OrderId,
                              UserId = grouped.Key.UserId,
                              TotalAmount = grouped.Key.TotalAmount,
                              Status = grouped.Key.Status,
                              CustomerName = grouped.Key.CustomerName,
                              Items = grouped.Select(g => new PackingOrderItemViewModel
                              {
                                  ProductName = g.i.Product,
                                  Quantity = g.oi.Quantity,
                                  Price = g.oi.Price
                              }).ToList()
                          }).ToList();

            return View(orders);
        }
        [HttpGet]
        public ActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarkAsPacked(int orderId)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = "Packed";
                order.PackedBy = Convert.ToInt32(Session["PackingPersonId"]); 
                db.SaveChanges();
            }
            return RedirectToAction("PendingOrders");
        }

        // GET: PackingPerson
        public ActionResult Index()
        {
            return View();
        }

    }

}