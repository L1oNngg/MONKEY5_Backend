using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class ReportImageDAO
    {
        public static List<ReportImage> GetReportImages()
        {
            var listImages = new List<ReportImage>();
            try
            {
                using var db = new MyDbContext();
                listImages = db.ReportImages
                    .Include(i => i.CompletionReport)
                    .ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listImages;
        }

        public static void SaveReportImage(ReportImage image)
        {
            try
            {
                using var context = new MyDbContext();
                context.ReportImages.Add(image);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateReportImage(ReportImage image)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<ReportImage>(image).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteReportImage(ReportImage image)
        {
            try
            {
                using var context = new MyDbContext();
                var imageToDelete = context.ReportImages.SingleOrDefault(i => i.ReportImageId == image.ReportImageId);
                if (imageToDelete != null)
                {
                    context.ReportImages.Remove(imageToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static ReportImage GetReportImageById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.ReportImages
                    .Include(i => i.CompletionReport)
                    .FirstOrDefault(i => i.ReportImageId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<ReportImage> GetReportImagesByReportId(Guid reportId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.ReportImages
                    .Include(i => i.CompletionReport)
                    .Where(i => i.ReportId.Equals(reportId))
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteReportImagesByReportId(Guid reportId)
        {
            try
            {
                using var context = new MyDbContext();
                var imagesToDelete = context.ReportImages.Where(i => i.ReportId == reportId).ToList();
                if (imagesToDelete.Any())
                {
                    context.ReportImages.RemoveRange(imagesToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static bool ImageExists(string imagePath)
        {
            try
            {
                using var db = new MyDbContext();
                return db.ReportImages.Any(i => i.ImagePath == imagePath);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
