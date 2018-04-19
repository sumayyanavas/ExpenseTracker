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
    public class IncomeSourceController : Controller
    {
        IncomeSourceBL incomeSourceBL = null;
        public IncomeSourceController(ExpenseTrackerContext context)
        {
            try
            {
                incomeSourceBL = new IncomeSourceBL(context);
                incomeSourceBL.AddIncomeSources();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
        }
        [HttpGet(Name = "GetAllIncomeSources")]
        public IEnumerable<IncomeSource> GetAlltIncomeSources()
        {
            
            List<IncomeSource> incomeSource = new List<IncomeSource>();
            try
            {                
                incomeSource = incomeSourceBL.GetIncomeSources();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting GetAlltIncomeSources", e);
            }
            return incomeSource;
        }
        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetIncomeSource")]
        public IActionResult GetById(long id)
        {
            IncomeSource incomeSourceObj = null;
            try
            {
                incomeSourceObj = new IncomeSource();
                incomeSourceObj = incomeSourceBL.GetIncomeSourceById(id);
                if (incomeSourceObj == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Get IncomeSource by id", e);
            }
            return new ObjectResult(incomeSourceObj);
        }
        // POST api/<controller>
        [HttpPost]
        public IActionResult AddNewIncomeSource([FromBody] IncomeSource incomeSource)
        {
            try
            {
                if (incomeSource == null)
                {
                    return BadRequest();
                }
                incomeSourceBL.AddNewIncomeSource(incomeSource);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Add new incomeSource", e);
            }

            return CreatedAtRoute("GetIncomeSource", new { id = incomeSource.ID }, incomeSource);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IncomeSource incomeSource)
        {
            bool result = false;
            try
            {
                if (incomeSource == null || incomeSource.ID != id)
                {
                    return BadRequest();
                }
                result = incomeSourceBL.UpdateIncomeSource(id, incomeSource);
                if (result == false)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Update incomeSource", e);
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
                result = incomeSourceBL.DeleteIncomeSource(id);
                if (result == false)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Delete incomeSource", e);
            }
            return new NoContentResult();
        }
    }
}
