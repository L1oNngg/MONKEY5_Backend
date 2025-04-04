using BusinessObjects;
using System;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IAvatarRepository
    {
        List<Avatar> GetAvatars();
        void SaveAvatar(Avatar avatar);
        void UpdateAvatar(Avatar avatar);
        void DeleteAvatar(Avatar avatar);
        Avatar GetAvatarById(Guid id);
        Avatar GetAvatarByUserId(Guid? userId);
    }
}
