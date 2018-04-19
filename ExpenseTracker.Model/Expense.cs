using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Model
{
    public class Expense
    {
        public int ID { get; set; }
        public int ProfileNo { get; set; }
        public bool IsItRecurring { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string PaymentType { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal TotalExpense { get; set; }
    }
}
