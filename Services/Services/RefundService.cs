using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository refundRepository;

        public RefundService()
        {
            refundRepository = new RefundRepository();
        }

        public List<Refund> GetRefunds() => refundRepository.GetRefunds();
        
        public void SaveRefund(Refund refund) => refundRepository.SaveRefund(refund);
        
        public void UpdateRefund(Refund refund) => refundRepository.UpdateRefund(refund);
        
        public void DeleteRefund(Refund refund) => refundRepository.DeleteRefund(refund);
        
        public Refund? GetRefundById(Guid id) => refundRepository.GetRefundById(id);
        
        public Refund? GetRefundByPaymentId(Guid paymentId) => refundRepository.GetRefundByPaymentId(paymentId);
        
        public List<Refund> GetRefundsByCustomerId(Guid customerId) => 
            refundRepository.GetRefundsByCustomerId(customerId);
        
        public List<Refund> GetRefundsByDateRange(DateTime startDate, DateTime endDate) => 
            refundRepository.GetRefundsByDateRange(startDate, endDate);
    }
}
