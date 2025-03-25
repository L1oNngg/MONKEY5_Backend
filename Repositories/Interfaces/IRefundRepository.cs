using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRefundRepository
    {
        List<Refund> GetRefunds();
        void SaveRefund(Refund refund);
        void UpdateRefund(Refund refund);
        void DeleteRefund(Refund refund);
        Refund? GetRefundById(Guid id);
        Refund? GetRefundByPaymentId(Guid paymentId);
        List<Refund> GetRefundsByCustomerId(Guid customerId);
        List<Refund> GetRefundsByDateRange(DateTime startDate, DateTime endDate);
    }
}
