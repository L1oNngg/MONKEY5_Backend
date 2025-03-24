using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class ServiceDAO
    {
        public static List<Service> GetServices()
        {
            var listServices = new List<Service>();
            try
            {
                using var db = new MyDbContext();
                listServices = db.Services.ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listServices;
        }

        public static void SaveService(Service service)
        {
            try
            {
                using var context = new MyDbContext();
                context.Services.Add(service);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateService(Service service)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<Service>(service).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteService(Service service)
        {
            try
            {
                using var context = new MyDbContext();
                var serviceToDelete = context.Services.SingleOrDefault(s => s.ServiceId == service.ServiceId);
                if (serviceToDelete != null)
                {
                    context.Services.Remove(serviceToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Service GetServiceById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Services.FirstOrDefault(s => s.ServiceId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Service> SearchServicesByName(string name)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Services
                    .Where(s => s.ServiceName.Contains(name))
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Service> GetServicesByPriceRange(decimal minPrice, decimal maxPrice)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Services
                    .Where(s => s.UnitPrice >= minPrice && s.UnitPrice <= maxPrice)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
