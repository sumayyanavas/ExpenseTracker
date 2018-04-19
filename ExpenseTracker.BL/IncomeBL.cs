using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExpenseTracker.Model;

namespace ExpenseTracker.BL
{
    public class IncomeBL
    {
        private readonly ExpenseTrackerContext _context;
        public IncomeBL(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public List<Income> GetIncomes()
        {
            return _context.Incomes.ToList();
        }
        public Income GetIncomeById(long id)
        {
            var income = _context.Incomes.FirstOrDefault(t => t.ID == id);
            return income;
        }
        public Income GetIncomeByIdDate(long ProfileNo, DateTime date)
        {
            var income = _context.Incomes.FirstOrDefault(t => t.ProfileNo == ProfileNo && t.Date==date);
            return income;
        }
        public FinancialSummary AddIncome(Income income)
        {
            decimal savingPercentage = income.Saving;
            decimal budgetPercentage = 100 - savingPercentage;
            income.Saving =  (income.Amount * (savingPercentage / 100));
            income.Budget =  (income.Amount * (budgetPercentage / 100));
            if (income.IsItRecurring == false)
            {
                income.Frequency = null;
            }
            _context.Incomes.Add(income);      
            _context.SaveChanges();
            FinancialSummary financialSummary = new FinancialSummary();
            financialSummary.Date = income.Date;
            financialSummary.ProfileNo = income.ProfileNo;          
            return FinancialSummaryDetails.GetfinancialSummary(financialSummary, _context);
        }
        public bool UpdateIncome(long id, Income income)
        {
            int result = 0;
            var updateIncome = GetIncomeById(id);
            if (updateIncome == null)
            {
                return false;
            }
            updateIncome.Saving = income.Amount * (income.Saving / 100);
            updateIncome.Budget = income.Amount * (income.Budget / 100);
            if (income.IsItRecurring == false)
            {
                updateIncome.Frequency = null;
            }

            updateIncome.Amount = income.Amount;
            updateIncome.IsItRecurring = income.IsItRecurring;
            updateIncome.IncomeSource = income.IncomeSource;
            updateIncome.Date = income.Date;
            _context.Incomes.Update(updateIncome);
            result = _context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteIncome(long id)
        {
            int result = 0;
            var deleteIncome = GetIncomeById(id);
            if (deleteIncome == null)
            {
                return false;
            }
            _context.Incomes.Remove(deleteIncome);
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
