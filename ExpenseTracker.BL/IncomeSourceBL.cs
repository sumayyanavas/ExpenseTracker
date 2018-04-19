using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExpenseTracker.Model;

namespace ExpenseTracker.BL
{
    public class IncomeSourceBL
    {
        private readonly ExpenseTrackerContext _context;
        public IncomeSourceBL(ExpenseTrackerContext context)
        {
            _context = context;
        }
        public List<IncomeSource> AddIncomeSources()
        {
            List<IncomeSource> incomeSources = new List<IncomeSource>();
            if (_context.IncomeSources.Count() == 0)
            {        
                incomeSources.Add(new IncomeSource { SourceName = "Salary" });
                incomeSources.Add(new IncomeSource { SourceName = "Interest" });
                incomeSources.Add(new IncomeSource { SourceName = "Dividend" });
                incomeSources.Add(new IncomeSource { SourceName = "Inheritance" });
                incomeSources.Add(new IncomeSource { SourceName = "Allowances" });
                incomeSources.Add(new IncomeSource { SourceName = "Other incomes" });
                _context.IncomeSources.AddRange(incomeSources);
                _context.SaveChanges();
            }
            return incomeSources;
        }

        public List<IncomeSource> GetIncomeSources()
        {
            return _context.IncomeSources.ToList();
        }
        public IncomeSource GetIncomeSourceById(long id)
        {
            var incomeSource = _context.IncomeSources.FirstOrDefault(t => t.ID == id);
            return incomeSource;
        }
        public void AddNewIncomeSource(IncomeSource incomeSource)
        {
            _context.IncomeSources.Add(incomeSource);
            _context.SaveChanges();
        }
        public bool UpdateIncomeSource(long id, IncomeSource incomeSource)
        {
            int result = 0;
            var updateIncomeSource = GetIncomeSourceById(id);
            if (updateIncomeSource == null)
            {
                return false;
            }
            updateIncomeSource.SourceName = incomeSource.SourceName;
            _context.IncomeSources.Update(updateIncomeSource);
            result = _context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteIncomeSource(long id)
        {
            int result = 0;
            var deleteIncomeSource = GetIncomeSourceById(id);
            if (deleteIncomeSource == null)
            {
                return false;
            }
            _context.IncomeSources.Remove(deleteIncomeSource);
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
