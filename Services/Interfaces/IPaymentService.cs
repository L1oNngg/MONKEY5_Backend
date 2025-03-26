using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IPaymentService
    {
        List<Payment> GetPayments();
        void SavePayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(Payment payment);
        Payment GetPaymentById(Guid id);
        Payment GetPaymentByBookingId(Guid bookingId);
        List<Payment> GetPaymentsByCustomerId(Guid customerId);
        List<Payment> GetPaymentsByStatus(PaymentStatus status);
        List<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate);
    }
}
