using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExpenseTracker.Model;

namespace ExpenseTracker.BL
{
    public class CurrencyBL
    {
        private readonly ExpenseTrackerContext _context;
        public CurrencyBL(ExpenseTrackerContext context)
        {
            _context = context;
        }
        public List<Currency> AddCurrency()
        {
            List<Currency> currencies = new List<Currency>();
            if (_context.Currencies.Count() == 0)
            {
                //Adding Basic Categories on startup
                currencies.Add(new Currency { CurrencyName = "Afghan afghani", CurrencyCode = "AFN" });
                currencies.Add(new Currency { CurrencyName = "European euro", CurrencyCode = "EUR" });
                currencies.Add(new Currency { CurrencyName = "Albanian lek", CurrencyCode = "ALL" });
                currencies.Add(new Currency { CurrencyName = "Algerian dinar", CurrencyCode = "DZD" });
                currencies.Add(new Currency { CurrencyName = "Angolan kwanza", CurrencyCode = "AOA" });
                currencies.Add(new Currency { CurrencyName = "Argentine peso", CurrencyCode = "ARS" });
                currencies.Add(new Currency { CurrencyName = "Armenian", CurrencyCode = "dram" });
                currencies.Add(new Currency { CurrencyName = "East Caribbean dollar", CurrencyCode = "XCD" });
                currencies.Add(new Currency { CurrencyName = "Aruban florin  ", CurrencyCode = "AWG" });
                currencies.Add(new Currency { CurrencyName = "Saint Helena pound", CurrencyCode = "SHP" });
                currencies.Add(new Currency { CurrencyName = "Bermudian dollar", CurrencyCode = "BMD" });
                currencies.Add(new Currency { CurrencyName = "Cayman Islands dollar", CurrencyCode = "KYD" });
                currencies.Add(new Currency { CurrencyName = "New Zealand dollar", CurrencyCode = "NZD" });
                currencies.Add(new Currency { CurrencyName = "Australian dollar", CurrencyCode = "AUD" });
                currencies.Add(new Currency { CurrencyName = "Cook Islands dollar", CurrencyCode = "none" });
                currencies.Add(new Currency { CurrencyName = "Netherlands Antillean guilder", CurrencyCode = "ANG" });
                currencies.Add(new Currency { CurrencyName = "Falkland Islands pound", CurrencyCode = "FKP" });
                currencies.Add(new Currency { CurrencyName = "Gibraltar pound", CurrencyCode = "GIP" });
                currencies.Add(new Currency { CurrencyName = "Danish krone", CurrencyCode = "DKK" });
                currencies.Add(new Currency { CurrencyName = "Guernsey Pound", CurrencyCode = "GGP" });
                currencies.Add(new Currency { CurrencyName = "Hong Kong dollar", CurrencyCode = "HKD" });
                currencies.Add(new Currency { CurrencyName = "Manx pound", CurrencyCode = "IMP" });
                currencies.Add(new Currency { CurrencyName = "Jersey pound", CurrencyCode = "JEP" });
                currencies.Add(new Currency { CurrencyName = "Kuwaiti dinar", CurrencyCode = "KWD" });
                currencies.Add(new Currency { CurrencyName = "Macanese pataca", CurrencyCode = "MOP" });
                currencies.Add(new Currency { CurrencyName = "East Caribbean dollar", CurrencyCode = "XCD" });
                currencies.Add(new Currency { CurrencyName = "CFP franc", CurrencyCode = "XPF" });
                currencies.Add(new Currency { CurrencyName = "Indian rupee", CurrencyCode = "INR" });
                _context.Currencies.AddRange(currencies);
                _context.SaveChanges();
            }
            return currencies;
        }
        public List<Currency> GetCurrencies()
        {
            return _context.Currencies.ToList();
        }
        
    }
}
