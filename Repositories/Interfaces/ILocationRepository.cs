using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface ILocationRepository
    {
        List<Location> GetLocations();
        void SaveLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(Location location);
        Location GetLocationById(Guid id);
        List<Location> SearchLocationsByCity(string city);
        List<Location> SearchLocationsByAddress(string address);
    }
}
