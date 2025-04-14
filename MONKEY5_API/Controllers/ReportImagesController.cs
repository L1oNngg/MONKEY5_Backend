using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportImagesController : ControllerBase
    {
        private readonly IReportImageService _reportImageService;

        public ReportImagesController()
        {
            _reportImageService = new ReportImageService();
        }

        // GET: api/ReportImages
        [HttpGet]
        public ActionResult<IEnumerable<ReportImage>> GetReportImages()
        {
            return _reportImageService.GetReportImages();
        }

        // GET: api/ReportImages/5
        [HttpGet("{id}")]
        public ActionResult<ReportImage> GetReportImage(Guid id)
        {
            var reportImage = _reportImageService.GetReportImageById(id);

            if (reportImage == null)
            {
                return NotFound();
            }

            return reportImage;
        }

        // GET: api/ReportImages/report/{reportId}
        [HttpGet("report/{reportId}")]
        public ActionResult<IEnumerable<ReportImage>> GetReportImagesByReportId(Guid reportId)
        {
            return _reportImageService.GetReportImagesByReportId(reportId);
        }

        // GET: api/ReportImages/file/{filename}
        [HttpGet("file/{filename}")]
        public IActionResult GetImageFile(string filename)
        {
            // Prevent directory traversal
            filename = Path.GetFileName(filename);
            
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", filename);
            
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            
            // Try to determine content type
            var contentType = "application/octet-stream"; // Default
            var extension = Path.GetExtension(filename).ToLowerInvariant();
            
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                // Add more types as needed
            }
            
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, contentType);
        }

        // PUT: api/ReportImages/5
        [HttpPut("{id}")]
        public IActionResult PutReportImage(Guid id, ReportImage reportImage)
        {
            if (id != reportImage.ReportImageId)
            {
                return BadRequest();
            }

            _reportImageService.UpdateReportImage(reportImage);

            return NoContent();
        }

        // POST: api/ReportImages/upload
        [HttpPost("upload")]
        public async Task<ActionResult<ReportImage>> UploadReportImage(IFormFile file, [FromForm] Guid reportId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded");
            }

            try
            {
                // Create directory if it doesn't exist
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Create a unique filename
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Create and save the ReportImage object
                var reportImage = new ReportImage
                {
                    ReportId = reportId,
                    ImagePath = fileName // Store just the filename
                };

                _reportImageService.SaveReportImage(reportImage);

                return CreatedAtAction("GetReportImage", new { id = reportImage.ReportImageId }, reportImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ReportImages
        [HttpPost]
        public ActionResult<ReportImage> PostReportImage(ReportImage reportImage)
        {
            _reportImageService.SaveReportImage(reportImage);

            return CreatedAtAction("GetReportImage", new { id = reportImage.ReportImageId }, reportImage);
        }

        // DELETE: api/ReportImages/5
        [HttpDelete("{id}")]
        public IActionResult DeleteReportImage(Guid id)
        {
            var reportImage = _reportImageService.GetReportImageById(id);
            if (reportImage == null)
            {
                return NotFound();
            }

            try
            {
                _reportImageService.DeleteReportImage(reportImage);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete report image because it is referenced by other records." });
                }
                if (ex.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete report image because it is referenced by other records." });
                }
                return Conflict(new { error = "Failed to delete report image. It may be referenced by other records. Details: " + ex.Message });
            }

            return NoContent();
        }

        // DELETE: api/ReportImages/report/{reportId}
        [HttpDelete("report/{reportId}")]
        public IActionResult DeleteReportImagesByReportId(Guid reportId)
        {
            _reportImageService.DeleteReportImagesByReportId(reportId);
            return NoContent();
        }
    }
}
