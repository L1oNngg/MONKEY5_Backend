using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using MONKEY5.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class AvatarDAO
    {
        public static List<Avatar> GetAvatars()
        {
            var listAvatars = new List<Avatar>();
            try
            {
                using var db = new MyDbContext();
                listAvatars = db.Avatars.ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listAvatars;
        }

        public static void SaveAvatar(Avatar avatar)
        {
            try
            {
                using var context = new MyDbContext();
                context.Avatars.Add(avatar);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateAvatar(Avatar avatar)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<Avatar>(avatar).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteAvatar(Avatar avatar)
        {
            try
            {
                using var context = new MyDbContext();
                var avatarToDelete = context.Avatars.SingleOrDefault(a => a.AvatarId == avatar.AvatarId);
                if (avatarToDelete != null)
                {
                    context.Avatars.Remove(avatarToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Avatar GetAvatarById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Avatars.FirstOrDefault(a => a.AvatarId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Avatar GetAvatarByUserId(Guid? userId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Avatars.FirstOrDefault(a => a.UserId.Equals(userId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
