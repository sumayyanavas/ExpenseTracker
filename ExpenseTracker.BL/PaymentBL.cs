using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExpenseTracker.Model;

namespace ExpenseTracker.BL
{
    public class PaymentBL
    {
        private readonly ExpenseTrackerContext _context;
        public PaymentBL(ExpenseTrackerContext context)
        {
            _context = context;
        }
        public List<Payment> AddPayments()
        {

            List<Payment> paymentList = new List<Payment>();
            if (_context.Payments.Count() == 0)
            {
                //Adding Basic Categories on startup
                paymentList.Add(new Payment { PaymentType = "Cash" });
                paymentList.Add(new Payment { PaymentType = "Visa" });
                paymentList.Add(new Payment { PaymentType = "Master" });
                paymentList.Add(new Payment { PaymentType = "AMEX" });
                paymentList.Add(new Payment { PaymentType = "Paypal" });
                _context.Payments.AddRange(paymentList);
                _context.SaveChanges();
            }

            return paymentList;
        }
        public List<Payment> GetPayments()
        {
            return _context.Payments.ToList();
        }
        public Payment GetPaymentById(long id)
        {
            var payment = _context.Payments.FirstOrDefault(t => t.ID == id);
            return payment;
        }
        public void AddNewPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }
        public bool UpdateCategoryName(long id, Payment payment)
        {
            int result = 0;
            var updatePayment = GetPaymentById(id);
            if (updatePayment == null)
            {
                return false;
            }
            updatePayment.PaymentType = payment.PaymentType;
            _context.Payments.Update(updatePayment);
            result = _context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public bool DeletePayment(long id)
        {
            int result = 0;
            var deletePayment = GetPaymentById(id);
            if (deletePayment == null)
            {
                return false;
            }
            _context.Payments.Remove(deletePayment);
            result = _context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }    
    }
}
