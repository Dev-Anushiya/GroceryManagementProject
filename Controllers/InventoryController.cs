using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryManagement1.Controllers
{
    public class InventoryController : Controller
    {
        private GroceryDbContext db = new GroceryDbContext();

        // GET: Inventory

        public ActionResult Index()
        {
            return View(db.Inventories.ToList());
        }

        public ActionResult AddToCart(int id)
        {
            var item = db.Inventories.Find(id);
            if (item != null && item.Stock > 0)
            {
                Session["Cart"] = item; // Simplified for demonstration
            }
            return RedirectToAction("Index");
        }
    }
}