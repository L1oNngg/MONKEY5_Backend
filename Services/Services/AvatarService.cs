using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Repositories.Interfaces;
using Repositories.Repos;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly IAvatarRepository _avatarRepository;

        public AvatarService()
        {
            _avatarRepository = new AvatarRepository();
        }

        public List<Avatar> GetAvatars() => _avatarRepository.GetAvatars();
        
        public void SaveAvatar(Avatar avatar) => _avatarRepository.SaveAvatar(avatar);
        
        public void UpdateAvatar(Avatar avatar) => _avatarRepository.UpdateAvatar(avatar);
        
        public void DeleteAvatar(Avatar avatar) => _avatarRepository.DeleteAvatar(avatar);
        
        public Avatar GetAvatarById(Guid id) => _avatarRepository.GetAvatarById(id);
        
        public Avatar GetAvatarByUserId(Guid? userId) => _avatarRepository.GetAvatarByUserId(userId);

        public async Task<Avatar> UploadAvatarImage(IFormFile file, Guid? userId)
        {
            // Create directory if it doesn't exist
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "avatars");
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

            // Check if user already has an avatar
            var existingAvatar = _avatarRepository.GetAvatarByUserId(userId);
            
            if (existingAvatar != null)
            {
                // Update existing avatar
                existingAvatar.AvatarImagePath = fileName;
                _avatarRepository.UpdateAvatar(existingAvatar);
                return existingAvatar;
            }
            else
            {
                // Create new avatar
                var avatar = new Avatar
                {
                    AvatarId = Guid.NewGuid(),
                    UserId = userId,
                    AvatarImagePath = fileName
                };

                _avatarRepository.SaveAvatar(avatar);
                return avatar;
            }
        }
    }
}
