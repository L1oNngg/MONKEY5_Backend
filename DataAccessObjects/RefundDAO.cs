using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class RefundDAO
    {
        public static List<Refund> GetRefunds()
        {
            var listRefunds = new List<Refund>();
            try
            {
                using var db = new MyDbContext();
                listRefunds = db.Refunds
                    .Include(r => r.Payment)
                    .ThenInclude(p => p.Booking)
                    .ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listRefunds;
        }

        public static void SaveRefund(Refund refund)
        {
            try
            {
                using var context = new MyDbContext();
                context.Refunds.Add(refund);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateRefund(Refund refund)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<Refund>(refund).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteRefund(Refund refund)
        {
            try
            {
                using var context = new MyDbContext();
                var refundToDelete = context.Refunds.SingleOrDefault(r => r.RefundId == refund.RefundId);
                if (refundToDelete != null)
                {
                    context.Refunds.Remove(refundToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Refund GetRefundById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Refunds
                    .Include(r => r.Payment)
                    .ThenInclude(p => p.Booking)
                    .FirstOrDefault(r => r.RefundId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Refund GetRefundByPaymentId(Guid paymentId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Refunds
                    .Include(r => r.Payment)
                    .ThenInclude(p => p.Booking)
                    .FirstOrDefault(r => r.PaymentId.Equals(paymentId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Refund> GetRefundsByCustomerId(Guid customerId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Refunds
                    .Include(r => r.Payment)
                    .ThenInclude(p => p.Booking)
                    .Where(r => r.Payment.Booking.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Refund> GetRefundsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Refunds
                    .Include(r => r.Payment)
                    .ThenInclude(p => p.Booking)
                    .Where(r => r.RefundDateTime >= startDate && r.RefundDateTime <= endDate)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
