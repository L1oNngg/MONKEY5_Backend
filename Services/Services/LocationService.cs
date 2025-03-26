using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;

        public LocationService()
        {
            locationRepository = new LocationRepository();
        }

        public List<Location> GetLocations() => locationRepository.GetLocations();
        
        public void SaveLocation(Location location) => locationRepository.SaveLocation(location);
        
        public void UpdateLocation(Location location) => locationRepository.UpdateLocation(location);
        
        public void DeleteLocation(Location location) => locationRepository.DeleteLocation(location);
        
        public Location GetLocationById(Guid id) => locationRepository.GetLocationById(id);
        
        public List<Location> SearchLocationsByCity(string city) => 
            locationRepository.SearchLocationsByCity(city);
        
        public List<Location> SearchLocationsByAddress(string address) => 
            locationRepository.SearchLocationsByAddress(address);
    }
}
