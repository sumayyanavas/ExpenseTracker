using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExpenseTracker.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.BL
{
    public class CategoryBL
    {
        private readonly ExpenseTrackerContext _context;
        public  CategoryBL(ExpenseTrackerContext context)
            {
            _context = context;
            }
        public List<Category> AddCategories()
        {
            List<Category> categories = new List<Category>();
            if (_context.Categories.Count() == 0)
            {
                //Adding Basic Categories on startup
                categories.Add(new Category { CategoryName = "Emi" });
                categories.Add(new Category { CategoryName = "Entertainment" });
                categories.Add(new Category { CategoryName = "Food" });
                categories.Add(new Category { CategoryName = "Memberships" });
                categories.Add(new Category { CategoryName = "Misc" });
                categories.Add(new Category { CategoryName = "Purchases" });
                categories.Add(new Category { CategoryName = "Subscriptions" });
                categories.Add(new Category { CategoryName = "Transport" });
                categories.Add(new Category { CategoryName = "Utility" });
                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }
            
            return categories;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public Category GetCategoryById(long id)
        {
            var category = _context.Categories.FirstOrDefault(t => t.ID == id);
            return category;
        }
        public void AddNewCategories(Category category)
        {
            _context.Categories.Add(category);
             _context.SaveChanges();
        }
        public bool UpdateCategoryName(long id, Category category)
        {
            int result=0;
            var updateCategory = GetCategoryById(id);
            if (updateCategory==null)
            {
                return false;
            }
            updateCategory.CategoryName = category.CategoryName;
            _context.Categories.Update(updateCategory);
            result= _context.SaveChanges();
            if(result==0)
              {
                  return false;
              }
            return true;
        }

        public bool DeleteCategoryById(long id)
        {
            int result = 0;
            var deleteCategory = GetCategoryById(id);
            if (deleteCategory == null)
            {
                return false;
            }
            _context.Categories.Remove(deleteCategory);
            result = _context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
            throw new NotImplementedException();
        }
    }
}
