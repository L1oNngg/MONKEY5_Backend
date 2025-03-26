using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IReportImageService
    {
        List<ReportImage> GetReportImages();
        void SaveReportImage(ReportImage image);
        void UpdateReportImage(ReportImage image);
        void DeleteReportImage(ReportImage image);
        ReportImage GetReportImageById(Guid id);
        List<ReportImage> GetReportImagesByReportId(Guid reportId);
        void DeleteReportImagesByReportId(Guid reportId);
        bool ImageExists(string imagePath);
    }
}
