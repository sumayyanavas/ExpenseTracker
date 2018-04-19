using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Model
{
    public class FinancialSummary
    {
        public int ProfileNo { get; set; }
        public decimal Income { get; set; }
        public decimal Savings { get; set; }
        public decimal Budget { get; set; }
        public decimal SavedPercentage { get; set; }
        public decimal SpendPercentage { get; set; }
        public DateTime Date { get; set; }
        public decimal Expense { get; set; }
        public String HealthMessage { get; set; }
    }
}
                                                                                                                                                                                                                                                       