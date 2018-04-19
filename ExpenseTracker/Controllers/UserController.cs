using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.BL;
using ExpenseTracker.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ExpenseTrackerContext _context;

        public UserController(ExpenseTrackerContext context)
        {
            _context = context;
            if (_context.Users.Count() == 0)
            {
                _context.Users.AddRange(new User { UserName = "sumayya", Password = "123" });//Set UserName and password startup
                _context.SaveChanges();
            }
        }
    }
}
