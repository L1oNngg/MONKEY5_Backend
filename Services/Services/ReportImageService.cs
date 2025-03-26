using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ReportImageService : IReportImageService
    {
        private readonly IReportImageRepository reportImageRepository;

        public ReportImageService()
        {
            reportImageRepository = new ReportImageRepository();
        }

        public List<ReportImage> GetReportImages() => reportImageRepository.GetReportImages();
        
        public void SaveReportImage(ReportImage image) => reportImageRepository.SaveReportImage(image);
        
        public void UpdateReportImage(ReportImage image) => reportImageRepository.UpdateReportImage(image);
        
        public void DeleteReportImage(ReportImage image) => reportImageRepository.DeleteReportImage(image);
        
        public ReportImage GetReportImageById(Guid id) => reportImageRepository.GetReportImageById(id);
        
        public List<ReportImage> GetReportImagesByReportId(Guid reportId) => 
            reportImageRepository.GetReportImagesByReportId(reportId);
        
        public void DeleteReportImagesByReportId(Guid reportId) => 
            reportImageRepository.DeleteReportImagesByReportId(reportId);
        
        public bool ImageExists(string imagePath) => reportImageRepository.ImageExists(imagePath);
    }
}
