using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GroceryManSystem.Models;
using GroceryManSystem.Services;
using GroceryManSystem.Services.Decorator;

namespace GroceryManagement1.Controllers
{
    public class UserController : Controller
    {
        private GroceryDbContext db = new GroceryDbContext();
        private readonly IUserService _userService;

        //public UserController()
        //{
        //    var db = new GroceryDbContext();
        //    _userService = new LoggingUserService(new UserService(db));
        //}
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Session["UserId"] = user.UserId;
                Session["Username"] = user.Username;
                Session["IsPremium"] = user.IsPremium;
                Session["PhoneNumber"] = user.PhoneNumber;
                Session["Emial"] = user.Email;
                return RedirectToAction("Index", "Inventory");
            }
            ViewBag.Error = "Invalid credentials";
            return View();
        }
    }
}