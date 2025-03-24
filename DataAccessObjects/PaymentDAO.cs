using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class PaymentDAO
    {
        public static List<Payment> GetPayments()
        {
            var listPayments = new List<Payment>();
            try
            {
                using var db = new MyDbContext();
                listPayments = db.Payments
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Service)
                    .ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listPayments;
        }

        public static void SavePayment(Payment payment)
        {
            try
            {
                using var context = new MyDbContext();
                context.Payments.Add(payment);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdatePayment(Payment payment)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<Payment>(payment).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeletePayment(Payment payment)
        {
            try
            {
                using var context = new MyDbContext();
                var paymentToDelete = context.Payments.SingleOrDefault(p => p.PaymentId == payment.PaymentId);
                if (paymentToDelete != null)
                {
                    context.Payments.Remove(paymentToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Payment GetPaymentById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Payments
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Service)
                    .FirstOrDefault(p => p.PaymentId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Payment GetPaymentByBookingId(Guid bookingId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Payments
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Service)
                    .FirstOrDefault(p => p.BookingId.Equals(bookingId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Payment> GetPaymentsByCustomerId(Guid customerId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Payments
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Service)
                    .Where(p => p.Booking.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Payment> GetPaymentsByStatus(PaymentStatus status)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Payments
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Service)
                    .Where(p => p.PaymentStatus == status)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Payments
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(p => p.Booking)
                    .ThenInclude(b => b.Service)
                    .Where(p => p.PaymentCreatedAt >= startDate && p.PaymentCreatedAt <= endDate)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
