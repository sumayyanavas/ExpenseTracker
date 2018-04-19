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
    public class CategoryController : Controller
    {
        
        CategoryBL catBL = null;
        public CategoryController(ExpenseTrackerContext context)
        {            
            try
            {
                catBL = new CategoryBL(context);
                catBL.AddCategories();              
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }           
        }
        [HttpGet(Name = "GetAllCategories")]
        public IEnumerable<Category> GetAlltCategories()
        {
            //CategoryBL catObj = null;
            List<Category> categories = new List<Category>();
            try
            {
                categories= catBL.GetCategories();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Context object", e);
            }
            return categories;
        }
        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult GetById(long id)
        {
            Category catObj = null; 
            try
            { 
                catObj= new Category();
                catObj = catBL.GetCategoryById(id);
                if (catObj == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Get category by id", e);
            }   
            return new ObjectResult(catObj);
        }
        // POST api/<controller>
        [HttpPost]
        public IActionResult AddNewCategory([FromBody] Category category)
        {
            try
            { 
                if (category == null)
                {
                    return BadRequest();
                }
                catBL.AddNewCategories(category);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Add new Category", e);
            }

            return CreatedAtRoute("GetCategory", new { id = category.ID }, category);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Category category)
        {
            bool result = false; 
            try
            {
                if (category == null || category.ID != id)
                {
                    return BadRequest();
                }
                result = catBL.UpdateCategoryName(id,category);
                if(result==false)
                {
                    return NotFound();
                }               
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting Add new Category", e);
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
                result = catBL.DeleteCategoryById(id);
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
