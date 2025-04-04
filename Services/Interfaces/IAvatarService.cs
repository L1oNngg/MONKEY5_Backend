using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces
{
    public interface IAvatarService
    {
        List<Avatar> GetAvatars();
        void SaveAvatar(Avatar avatar);
        void UpdateAvatar(Avatar avatar);
        void DeleteAvatar(Avatar avatar);
        Avatar GetAvatarById(Guid id);
        Avatar GetAvatarByUserId(Guid? userId);
        Task<Avatar> UploadAvatarImage(IFormFile file, Guid? userId);
    }
}
