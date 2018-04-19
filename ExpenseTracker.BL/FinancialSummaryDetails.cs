using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseTracker.Model;

namespace ExpenseTracker.BL
{
    public class FinancialSummaryDetails
    {
        public static FinancialSummary GetfinancialSummary(FinancialSummary financialSummary, ExpenseTrackerContext _context)
        {
            FinancialSummary financialSummaryObj = null;
            decimal currentMonthexpense=0;
            try
            {
                //find current month total incomes and total budget
                var currentMonthFinancialObj = _context.Incomes.Where(b => b.Date.Month == financialSummary.Date.Month && b.Date.Year == financialSummary.Date.Year && b.ProfileNo == financialSummary.ProfileNo);
                decimal currentMonthIncome = currentMonthFinancialObj.Sum(d => d.Amount);
                decimal currentMonthBudget = currentMonthFinancialObj.Sum(d => d.Budget);

                var currentMonthExpenseObj = _context.Expenses.Where(b => b.ExpenseDate.Month == financialSummary.Date.Month && b.ExpenseDate.Year == financialSummary.Date.Year && b.ProfileNo == financialSummary.ProfileNo);
                currentMonthexpense = currentMonthExpenseObj.Sum(d => d.Amount);

                //
                var incomesObj = _context.Incomes.Where(b => b.Date.Month <= financialSummary.Date.Month && b.Date.Year <= financialSummary.Date.Year && b.ProfileNo == financialSummary.ProfileNo);
                var expenseObj = _context.Expenses.Where(b => b.ExpenseDate.Month < financialSummary.Date.Month && b.ExpenseDate.Year <= financialSummary.Date.Year && b.ProfileNo == financialSummary.ProfileNo);
                decimal totalIncome = 0;
                decimal totalExpense = 0;
                decimal totalSavings = 0;
                if (incomesObj != null)
                {
                    totalIncome = incomesObj.Sum(d => d.Amount);
                }
                if (expenseObj != null)
                {
                    totalExpense = expenseObj.Sum(d => d.Amount);
                }
                totalSavings = (totalIncome - totalExpense) - currentMonthBudget;
                financialSummaryObj = new FinancialSummary();
                financialSummaryObj.Income = currentMonthIncome;
                if (currentMonthexpense >= currentMonthBudget)
                {
                    financialSummaryObj.Budget = 0;
                    financialSummaryObj.Savings = totalSavings - (currentMonthexpense- currentMonthBudget);                    
                }
                else
                {
                    financialSummaryObj.Budget = currentMonthBudget - currentMonthexpense;
                    financialSummaryObj.Savings = totalSavings;
                    
                }
                //Financial Health
                GetFinancialHealth(financialSummaryObj, currentMonthexpense, currentMonthBudget);


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getting financial summary", e);
            }
            return financialSummaryObj;
        }

        public  static List<Income> GetAllIncomes(FinancialSummary financialSummary, ExpenseTrackerContext _context)
        {
            var currentMonthFinancialObj = _context.Incomes.Where(b => b.Date.Month == financialSummary.Date.Month && b.Date.Year == financialSummary.Date.Year && b.ProfileNo == financialSummary.ProfileNo);
            return currentMonthFinancialObj.ToList();
            //throw new NotImplementedException();
        }

        private static FinancialSummary GetFinancialHealth(FinancialSummary financialSummaryObj,
            decimal currentMonthExpense,decimal currentMonthBudget)
        {
            decimal currrentMonthSavings = financialSummaryObj.Income - currentMonthBudget;

            if (currentMonthExpense ==0)
            {
                financialSummaryObj.HealthMessage = "You do not have any expenses.";
                financialSummaryObj.SavedPercentage = 100;
                financialSummaryObj.SpendPercentage = 0;
                return financialSummaryObj;
            }
            else if (currentMonthBudget == 0)
            {
                currentMonthBudget = currrentMonthSavings;
            }
            if (currentMonthExpense > currentMonthBudget)
            {
                financialSummaryObj.SavedPercentage = 0;
                financialSummaryObj.SpendPercentage = 100;
                financialSummaryObj.HealthMessage = "You have spent all of your budget.Your saving at risk.";
                return financialSummaryObj;
            }
           


            decimal spendingPercentage = currentMonthBudget / currentMonthExpense;
            spendingPercentage = 100 / spendingPercentage;
            financialSummaryObj.SpendPercentage = spendingPercentage;

           
            decimal savingPercentage = financialSummaryObj.Income / currrentMonthSavings;
            savingPercentage = 100 / savingPercentage;
            financialSummaryObj.SavedPercentage = savingPercentage;

            if(spendingPercentage>savingPercentage)
                financialSummaryObj.HealthMessage = "You can save more.Cut down expenses.";
            else
                financialSummaryObj.HealthMessage = "You are in good financial position.";

            return financialSummaryObj;
        }
    }
}
