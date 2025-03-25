using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class ReportImageRepository : IReportImageRepository
    {
        public List<ReportImage> GetReportImages() => ReportImageDAO.GetReportImages();
        
        public void SaveReportImage(ReportImage image) => ReportImageDAO.SaveReportImage(image);
        
        public void UpdateReportImage(ReportImage image) => ReportImageDAO.UpdateReportImage(image);
        
        public void DeleteReportImage(ReportImage image) => ReportImageDAO.DeleteReportImage(image);
        
        public ReportImage GetReportImageById(Guid id) => ReportImageDAO.GetReportImageById(id);
        
        public List<ReportImage> GetReportImagesByReportId(Guid reportId) => 
            ReportImageDAO.GetReportImagesByReportId(reportId);
        
        public void DeleteReportImagesByReportId(Guid reportId) => 
            ReportImageDAO.DeleteReportImagesByReportId(reportId);
        
        public bool ImageExists(string imagePath) => ReportImageDAO.ImageExists(imagePath);
    }
}
