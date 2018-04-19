using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Model
{
    public class Income
    {
        public int ID { get; set; }
        public int ProfileNo { get; set; }
        public decimal Amount { get; set; }
        public decimal Saving { get; set; }
        public decimal Budget { get; set; }
        public bool IsItRecurring { get; set; }
        public string Frequency { get; set; }
        public string IncomeSource { get; set; }
        public DateTime Date { get; set; }
    }
}
