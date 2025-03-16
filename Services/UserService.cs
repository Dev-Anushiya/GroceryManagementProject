using GroceryManSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GroceryManSystem.Services
{
    public class UserService : IUserService
    {
        private readonly GroceryDbContext _db;

        public UserService(GroceryDbContext db)
        {
            _db = db;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User> RegisterAsync(User user)  
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }

}