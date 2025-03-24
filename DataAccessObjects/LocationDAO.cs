using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class LocationDAO
    {
        public static List<Location> GetLocations()
        {
            var listLocations = new List<Location>();
            try
            {
                using var db = new MyDbContext();
                listLocations = db.Locations.ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listLocations;
        }

        public static void SaveLocation(Location location)
        {
            try
            {
                using var context = new MyDbContext();
                context.Locations.Add(location);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateLocation(Location location)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<Location>(location).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteLocation(Location location)
        {
            try
            {
                using var context = new MyDbContext();
                var locationToDelete = context.Locations.SingleOrDefault(l => l.LocationId == location.LocationId);
                if (locationToDelete != null)
                {
                    context.Locations.Remove(locationToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Location GetLocationById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Locations.FirstOrDefault(l => l.LocationId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Location> SearchLocationsByCity(string city)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Locations
                    .Where(l => l.City.Contains(city))
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Location> SearchLocationsByAddress(string address)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Locations
                    .Where(l => l.Address.Contains(address))
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
