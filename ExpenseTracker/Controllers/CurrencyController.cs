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
    public class CurrencyController : Controller
    {
        CurrencyBL currencyBL= null;
        public CurrencyController(ExpenseTrackerContext context)
        {
            try
            {
                currencyBL = new CurrencyBL(context);
                currencyBL.AddCurrency();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
        }
        [HttpGet(Name = "GetAllCurrencies")]
        public IEnumerable<Currency> GetAllCurrency()
        {
            //CategoryBL catObj = null;
            List<Currency> currencies = new List<Currency>();
            try
            {
                currencies = currencyBL.GetCurrencies();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
            return currencies;
        }       
    }
}
