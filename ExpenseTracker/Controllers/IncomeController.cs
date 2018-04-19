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
    public class IncomeController : Controller
    {
        IncomeBL incomeBL = null;
        public IncomeController(ExpenseTrackerContext context)
        {
            try
            {
                incomeBL = new IncomeBL(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
        }
        [HttpGet(Name = "GetAllIncomes")]
        public IEnumerable<Income> GetAllIncomes()
        {
            
            List<Income> incomes = new List<Income>();
            try
            {
                incomes = incomeBL.GetIncomes();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
            return incomes;
        }
        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetIncome")]
        public IActionResult GetById(long id)
        {
            Income incomeObj = null;
            try
            {
                incomeObj = new Income();
                incomeObj = incomeBL.GetIncomeById(id);
                if (incomeObj == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Get Income by id", e);
            }
            return new ObjectResult(incomeObj);
        }
        // POST api/<controller>
        [HttpPost]
        public IActionResult AddNewIncome([FromBody] Income income)
        {
            try
            {
                if (income == null)
                {
                    return BadRequest();
                }
                return new ObjectResult(incomeBL.AddIncome(income));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Add new Income", e);
            }

            return new ObjectResult("Exception in test");
        }

        
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Income income)
        {
            bool result = false;
            try
            {
                if (income == null || income.ID != id)
                {
                    return BadRequest();
                }
                result = incomeBL.UpdateIncome(id, income);
                if (result == false)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Update new Income", e);
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
                result = incomeBL.DeleteIncome(id);
                if (result == false)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Delete Income", e);
            }
            return new NoContentResult();
        }

    }
}
