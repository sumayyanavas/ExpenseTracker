using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExpenseTracker.Model;

namespace ExpenseTracker.BL
{
    public class ExpenseBL
    {
        private readonly ExpenseTrackerContext _context;
        public ExpenseBL(ExpenseTrackerContext context)
        {
            _context = context;
        }
        public List<Expense> GetExpenses()
        {
            return _context.Expenses.ToList();
        }
        public List<Expense> GetExpenseByIdDate(long ProfileNo, DateTime date)
        {
            var expenses = _context.Expenses.Where(t => t.ProfileNo == ProfileNo && t.ExpenseDate.Month==date.Month && t.ExpenseDate.Year == date.Year);
            if (expenses==null)
            {
                return null;
            }
            var totalExpense = expenses.Sum(d => d.Amount);
            return expenses.ToList(); 
        }
        public Expense GetExpenseById(long id)
        {
            var expenses = _context.Expenses.FirstOrDefault(t => t.ID == id);
            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }
        public List<Expense> AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
            var expenses = GetExpenseByIdDate(expense.ProfileNo, expense.ExpenseDate);
            if (expenses == null)
            {
                return null;
            }
            return expenses.ToList();
        }
        public bool UpdateExpense(long id, Expense expense)
        {
            int result = 0;
            var updateExpense = GetExpenseById(id);
            if (updateExpense == null)
            {
                return false;
            }
            updateExpense.ExpenseName = expense.ExpenseName;
            updateExpense.Amount = expense.Amount;
            updateExpense.Category = expense.Category;
            updateExpense.PaymentType = expense.PaymentType;
            updateExpense.ExpenseDate = expense.ExpenseDate;
            _context.Expenses.Update(updateExpense);
            result = _context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteExpense(long id)
        {
            int result = 0;
            var deleteExpense = GetExpenseById(id);
            if (deleteExpense == null)
            {
                return false;
            }
            _context.Expenses.Remove(deleteExpense);
            result = _context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
            throw new NotImplementedException();
        }

        public bool ValidateExpense(Expense expense)
        {
            Expense expenseObj = null;
            try
            {
                expenseObj = new Expense();
                decimal totExpense = totalExpense(expense);

                var currentMonthIncomeObj = _context.Incomes.Where(b => b.Date.Month == expense.ExpenseDate.Month && b.Date.Year == expense.ExpenseDate.Year && b.ProfileNo == expense.ProfileNo);
                decimal currentMonthBudget = currentMonthIncomeObj.Sum(d => d.Budget);

                if (totExpense <= currentMonthBudget)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting checking budget", e);
            }
            return false;
            throw new NotImplementedException();
        }
        public decimal totalExpense(Expense expense)
        {
            decimal totalExpense = 0;
            try
            {
                var currentMonthExpenseObj = _context.Expenses.Where(b => b.ExpenseDate.Month == expense.ExpenseDate.Month && b.ExpenseDate.Year == expense.ExpenseDate.Year && b.ProfileNo == expense.ProfileNo);
                decimal currentMonthExpense = currentMonthExpenseObj.Sum(d => d.Amount);
                totalExpense = currentMonthExpense + expense.Amount;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting total expense", e);
            }
            return totalExpense;
        }

    }
}
