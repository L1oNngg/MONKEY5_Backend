using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class LocationRepository : ILocationRepository
    {
        public List<Location> GetLocations() => LocationDAO.GetLocations();
        
        public void SaveLocation(Location location) => LocationDAO.SaveLocation(location);
        
        public void UpdateLocation(Location location) => LocationDAO.UpdateLocation(location);
        
        public void DeleteLocation(Location location) => LocationDAO.DeleteLocation(location);
        
        public Location GetLocationById(Guid id) => LocationDAO.GetLocationById(id);
        
        public List<Location> SearchLocationsByCity(string city) => 
            LocationDAO.SearchLocationsByCity(city);
        
        public List<Location> SearchLocationsByAddress(string address) => 
            LocationDAO.SearchLocationsByAddress(address);
    }
}
