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
    public class PaymentController : Controller
    {
         PaymentBL paymentBL = null;
        public PaymentController(ExpenseTrackerContext context)
        {
            try
            {
                paymentBL = new PaymentBL(context);
                paymentBL.AddPayments();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
        }
        [HttpGet(Name = "GetAllPayments")]
        public IEnumerable<Payment> GetAllPayments()
        {
            //CategoryBL catObj = null;
            List<Payment> paymentList = new List<Payment>();
            try
            {
                paymentList = paymentBL.GetPayments();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Get All Payments", e);
            }
            return paymentList;
        }
        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetPayment")]
        public IActionResult GetById(long id)
        {
            Payment PaymentObj = null;
            try
            {
                PaymentObj = new Payment();
                PaymentObj = paymentBL.GetPaymentById(id);
                if (PaymentObj == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Get Payment by id", e);
            }
            return new ObjectResult(PaymentObj);
        }
        // POST api/<controller>
        [HttpPost]
        public IActionResult AddNewPayment([FromBody] Payment payment)
        {
            try
            {
                if (payment == null)
                {
                    return BadRequest();
                }
                paymentBL.AddNewPayment(payment);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Add new Payment", e);
            }

            return CreatedAtRoute("GetPayment", new { id = payment.ID }, payment);
        }
       
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Payment payment)
        {
            bool result = false;
            try
            {
                if (payment == null || payment.ID != id)
                {
                    return BadRequest();
                }
                result = paymentBL.UpdateCategoryName(id, payment);
                if (result == false)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Add payment", e);
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
                result = paymentBL.DeletePayment(id);
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
