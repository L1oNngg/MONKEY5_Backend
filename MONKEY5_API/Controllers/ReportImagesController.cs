using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;

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

        // GET: api/ReportImages/exists/{imagePath}
        [HttpGet("exists/{imagePath}")]
        public ActionResult<bool> ImageExists(string imagePath)
        {
            return _reportImageService.ImageExists(imagePath);
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

            _reportImageService.DeleteReportImage(reportImage);

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
