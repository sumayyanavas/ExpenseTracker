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
    public class FinancialController : Controller
    {
        FinancialSummaryBL financialSummaryBL = null;
        public FinancialController(ExpenseTrackerContext context)
        {
            try
            {
                financialSummaryBL = new FinancialSummaryBL(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
        }
        // POST api/<controller>
        [HttpPost]
        public IActionResult GetFinacialSummary([FromBody]FinancialSummary financialSummary)
        {
            FinancialSummary financialObj = null;
            try
            {
                financialObj = new FinancialSummary();

                financialObj = financialSummaryBL.GetfinancialSummary(financialSummary);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Financial summary", e);
            }
            return new ObjectResult(financialObj);
        }
       
        // PUT api/<controller>/5
        [HttpPut("{profileNo}")]
        public IEnumerable<Income> Update(int profileNo, [FromBody] FinancialSummary financialSummary)
        {

            FinancialSummary financialObj = null;
            List<Income> incomes = null; 
            try
            {
                incomes = new List<Income>();
                financialObj = new FinancialSummary();
                incomes = financialSummaryBL.GetAllIncomes(profileNo, financialSummary);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
            return incomes;
        }

        
    }
}
