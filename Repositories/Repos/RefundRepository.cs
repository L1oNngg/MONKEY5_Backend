using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class RefundRepository : IRefundRepository
    {
        public List<Refund> GetRefunds() => RefundDAO.GetRefunds();
        
        public void SaveRefund(Refund refund) => RefundDAO.SaveRefund(refund);
        
        public void UpdateRefund(Refund refund) => RefundDAO.UpdateRefund(refund);
        
        public void DeleteRefund(Refund refund) => RefundDAO.DeleteRefund(refund);
        
        public Refund? GetRefundById(Guid id) => RefundDAO.GetRefundById(id);
        
        public Refund? GetRefundByPaymentId(Guid paymentId) => RefundDAO.GetRefundByPaymentId(paymentId);
        
        public List<Refund> GetRefundsByCustomerId(Guid customerId) => RefundDAO.GetRefundsByCustomerId(customerId);
        
        public List<Refund> GetRefundsByDateRange(DateTime startDate, DateTime endDate) => 
            RefundDAO.GetRefundsByDateRange(startDate, endDate);
    }
}
