using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentService()
        {
            paymentRepository = new PaymentRepository();
        }

        public List<Payment> GetPayments() => paymentRepository.GetPayments();
        
        public void SavePayment(Payment payment) => paymentRepository.SavePayment(payment);
        
        public void UpdatePayment(Payment payment) => paymentRepository.UpdatePayment(payment);
        
        public void DeletePayment(Payment payment) => paymentRepository.DeletePayment(payment);
        
        public Payment GetPaymentById(Guid id) => paymentRepository.GetPaymentById(id);
        
        public Payment GetPaymentByBookingId(Guid bookingId) => 
            paymentRepository.GetPaymentByBookingId(bookingId);
        
        public List<Payment> GetPaymentsByCustomerId(Guid customerId) => 
            paymentRepository.GetPaymentsByCustomerId(customerId);
        
        public List<Payment> GetPaymentsByStatus(PaymentStatus status) => 
            paymentRepository.GetPaymentsByStatus(status);
        
        public List<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate) => 
            paymentRepository.GetPaymentsByDateRange(startDate, endDate);
    }
}
