using GroceryManSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GroceryManSystem.Services.Decorator
{
    public class LoggingUserService 
    {
        private readonly IUserService _innerService;

        public LoggingUserService(IUserService innerService)
        {
            _innerService = innerService;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _innerService.AuthenticateAsync(username, password);
            return user;
        }
    }
}