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
    public class ExpenseController : Controller
    {
        ExpenseBL expenseBL = null;
        public ExpenseController(ExpenseTrackerContext context)
        {
            try
            {
                expenseBL = new ExpenseBL(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
        }
        [HttpGet(Name = "GetAllexpenses")]
        public IEnumerable<Expense> GetAlltexpenses()
        {

            List<Expense> expenses = null; 
            try
            {
                expenses = new List<Expense>();
                expenses = expenseBL.GetExpenses();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting GetAlltexpenses", e);
            }
            return expenses.ToList();
        }
        // GET api/<controller>/5
        [HttpGet("{ProfileNo},{Date}", Name = "GetExpense")]
        public IActionResult GetByIdDate(long ProfileNo, DateTime date)
        {
            List<Expense> expenses = null;
            try
            {
                expenses = new List<Expense>();
                expenses = expenseBL.GetExpenseByIdDate(ProfileNo, date);
                if (expenses == null)
                {
                    return new ObjectResult("NO DATA");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Get category by id", e);
            }
            return new ObjectResult(expenses);
        }
        // POST api/<controller>
        [HttpPost]
        public IActionResult AddExpenses([FromBody] Expense expense)
        {
            List<Expense> expenses = null;
            try
            {
                expenses = new List<Expense>();
                if (expense == null)
                {
                    return BadRequest();
                }

                expenses=expenseBL.AddExpense(expense);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Add new expense", e);
            }

            return new ObjectResult(expenses);
        }
        [HttpPost("{profileNo}")]
        public IActionResult ValidateExpense(long profileNo,[FromBody] Expense expense)
        {
            bool result = false;
            try
            {

                if (expense == null)
                {
                    return BadRequest();
                }

                result = expenseBL.ValidateExpense(expense);
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Add new expense", e);
            }

            return new ObjectResult(false);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Expense expense)
        {
            bool result = false;
            try
            {
                if (expense == null || expense.ID != id)
                {
                    return BadRequest();
                }
                result = expenseBL.UpdateExpense(id, expense);
                if (result == false)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting UpdateExpense", e);
            }
            return new NoContentResult();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            bool result = false;
            try
            {
                result = expenseBL.DeleteExpense(id);
                if (result == false)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Get category by id", e);
            }
            return new NoContentResult();
        }
    }
}
