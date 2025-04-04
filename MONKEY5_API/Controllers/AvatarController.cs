using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly IAvatarService _avatarService;

        public AvatarController()
        {
            _avatarService = new AvatarService();
        }

        // GET: api/Avatar
        [HttpGet]
        public ActionResult<IEnumerable<Avatar>> GetAvatars()
        {
            return _avatarService.GetAvatars();
        }

        // GET: api/Avatar/5
        [HttpGet("{id}")]
        public ActionResult<Avatar> GetAvatar(Guid id)
        {
            var avatar = _avatarService.GetAvatarById(id);

            if (avatar == null)
            {
                return NotFound();
            }

            return avatar;
        }

        // GET: api/Avatar/user/5
        [HttpGet("user/{userId}")]
        public ActionResult<Avatar> GetAvatarByUserId(Guid userId)
        {
            var avatar = _avatarService.GetAvatarByUserId(userId);

            if (avatar == null)
            {
                return NotFound();
            }

            return avatar;
        }

        // GET: api/Avatar/file/{filename}
        [HttpGet("file/{filename}")]
        public IActionResult GetAvatarFile(string filename)
        {
            // Prevent directory traversal
            filename = Path.GetFileName(filename);
            
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "avatars", filename);
            
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

        // PUT: api/Avatar/5
        [HttpPut("{id}")]
        public IActionResult PutAvatar(Guid id, Avatar avatar)
        {
            if (id != avatar.AvatarId)
            {
                return BadRequest();
            }

            _avatarService.UpdateAvatar(avatar);

            return NoContent();
        }

        // POST: api/Avatar/upload
        [HttpPost("upload")]
        public async Task<ActionResult<Avatar>> UploadAvatar(IFormFile file, [FromForm] Guid? userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded");
            }

            try
            {
                var avatar = await _avatarService.UploadAvatarImage(file, userId);
                return CreatedAtAction("GetAvatar", new { id = avatar.AvatarId }, avatar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Avatar
        [HttpPost]
        public ActionResult<Avatar> PostAvatar(Avatar avatar)
        {
            _avatarService.SaveAvatar(avatar);

            return CreatedAtAction("GetAvatar", new { id = avatar.AvatarId }, avatar);
        }

        // DELETE: api/Avatar/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAvatar(Guid id)
        {
            var avatar = _avatarService.GetAvatarById(id);
            if (avatar == null)
            {
                return NotFound();
            }

            _avatarService.DeleteAvatar(avatar);

            return NoContent();
        }
    }
}
