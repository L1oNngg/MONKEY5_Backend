using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public List<Payment> GetPayments() => PaymentDAO.GetPayments();
        
        public void SavePayment(Payment payment) => PaymentDAO.SavePayment(payment);
        
        public void UpdatePayment(Payment payment) => PaymentDAO.UpdatePayment(payment);
        
        public void DeletePayment(Payment payment) => PaymentDAO.DeletePayment(payment);
        
        public Payment GetPaymentById(Guid id) => PaymentDAO.GetPaymentById(id);
        
        public Payment GetPaymentByBookingId(Guid bookingId) => 
            PaymentDAO.GetPaymentByBookingId(bookingId);
        
        public List<Payment> GetPaymentsByCustomerId(Guid customerId) => 
            PaymentDAO.GetPaymentsByCustomerId(customerId);
        
        public List<Payment> GetPaymentsByStatus(PaymentStatus status) => 
            PaymentDAO.GetPaymentsByStatus(status);
        
        public List<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate) => 
            PaymentDAO.GetPaymentsByDateRange(startDate, endDate);
    }
}
