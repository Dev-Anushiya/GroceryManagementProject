using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GroceryManSystem.Models;
using GroceryManSystem.Repository.Implementation;
namespace GroceryManSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;

        // GET: Admin
        GroceryDbContext db = new GroceryDbContext();
        //public AdminController()
        //{
        //    var dbContext = new GroceryDbContext();
        //    _userRepository = new UserRepository(dbContext);
        //}
        //public AdminController(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}

        public async Task<ActionResult> Users()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return View(users);
        }
        public ActionResult Index()
        {
            return View();
        }
        // View Dashboard
        public ActionResult Dashboard()
        {
            // Retrieve total user count
            var totalUsers = db.Users.Count();

            // Retrieve total order count
            var totalOrders = db.Orders.Count();

            // Retrieve low stock products (Stock <= 0)
            var lowStockProducts = db.Inventories
                .Where(p => p.Stock <= 0)
                .Select(p => new ProductViewModel
                {
                    ProductName = p.Product,
                    Stock = p.Stock
                }).ToList();

            // Retrieve pending orders (Status == "Pending")
            var pendingOrders = db.Orders
                .Where(o => o.Status == "Pending")
                .Include(o => o.Items.Select(oi => oi.Product)) // Include related order items and products
                .Select(o => new PendingOrderViewModel
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    TotalAmount = o.TotalAmount,
                    CreatedAt = o.CreatedAt,
                    Status = o.Status,
                    OrderItems = o.Items.Select(oi => new OrderItemViewModel
                    {
                        ProductName = oi.Product.Product,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                }).ToList();

            // Populate the dashboard view model
            var viewModel = new DashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalOrders = totalOrders,
                LowStockProducts = lowStockProducts,
                PendingOrders = pendingOrders
            };

            // Pass the view model to the view
            return View(viewModel);
        }

        // View All Users
        //public ActionResult Users()
        //{
        //    var users = db.Users.ToList();
        //    return View(users);
        //}

        // View All Orders
        public ActionResult Orders(string status = null)
        {
            var orders = string.IsNullOrEmpty(status)
                ? db.Orders.Include(o => o.Items).ToList()
                : db.Orders.Include(o => o.Items).Where(o => o.Status == status).ToList();

            return View(orders);
        }

        // View and Edit Products
        public ActionResult Products()
        {
            var products = db.Inventories.ToList();
            return View(products);
        }

        public ActionResult OrderDetails(int orderId)
        {
            // Fetch the order details based on orderId
            var order = db.Orders
                .Include(o => o.Items.Select(oi => oi.Product)) // Include related OrderItems and Product data
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return HttpNotFound(); // Handle the case when the order is not found
            }
            order.User = db.Users.FirstOrDefault(u => u.UserId == order.UserId);
            //order.User.Username = db.Users.Where(u => u.UserId == order.UserId).Select(u => u.Username).FirstOrDefault(); 
            //order.User.Email = db.Users.Where(u => u.UserId == order.UserId).Select(u => u.Email).FirstOrDefault(); 
            return View(order); // Pass the order to the view
        }

        [HttpPost]
        public ActionResult UpdateOrderStatus(int orderId, string status)
        {
            var order = db.Orders.Find(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            order.Status = status;
            db.SaveChanges();

            return RedirectToAction("Orders");
        }


        [HttpPost]
        public async Task<ActionResult> EditProduct(int productId, int newStock)
        {
            var product = await db.Inventories.FindAsync(productId);
            if (product != null)
            {
                product.Stock = newStock;
                await db.SaveChangesAsync();
                TempData["Message"] = "Stock updated successfully.";
            }

            return RedirectToAction("Products");
        }
    }
}