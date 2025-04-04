using BusinessObjects;
using DataAccessObjects;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace Repositories.Repos
{
    public class AvatarRepository : IAvatarRepository
    {
        public List<Avatar> GetAvatars() => AvatarDAO.GetAvatars();
        
        public void SaveAvatar(Avatar avatar) => AvatarDAO.SaveAvatar(avatar);
        
        public void UpdateAvatar(Avatar avatar) => AvatarDAO.UpdateAvatar(avatar);
        
        public void DeleteAvatar(Avatar avatar) => AvatarDAO.DeleteAvatar(avatar);
        
        public Avatar GetAvatarById(Guid id) => AvatarDAO.GetAvatarById(id);
        
        public Avatar GetAvatarByUserId(Guid? userId) => AvatarDAO.GetAvatarByUserId(userId);
    }
}
