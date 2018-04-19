using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExpenseTracker.Model;

namespace ExpenseTracker.BL
{
    public class FinancialSummaryBL
    {
        private readonly ExpenseTrackerContext _context;
        public FinancialSummaryBL(ExpenseTrackerContext context)
        {
            _context = context;
        }
        public FinancialSummary GetfinancialSummary(FinancialSummary financialSummary)
        {
            return FinancialSummaryDetails.GetfinancialSummary(financialSummary, _context);
        }
        public List<Income> GetAllIncomes(int profileNo, FinancialSummary financialSummary)
        {
            financialSummary.ProfileNo = profileNo;
            return FinancialSummaryDetails.GetAllIncomes(financialSummary, _context);
        }
    }
}
