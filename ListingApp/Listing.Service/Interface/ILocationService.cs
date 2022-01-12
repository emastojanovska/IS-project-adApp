using Listing.Domain.DomainModels;
using System;
using System.Collections.Generic;

namespace Listing.Service.Interface
{
    public interface ILocationService
    {
        List<Location> GetAllLocations();
        void CreateNewLocation(Location location);
        void UpdateExistingLocation(Location location);
        void DeleteLocation(Guid id);
        Location GetDetailsForLocation(Guid? id);
    }
}
