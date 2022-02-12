using Listing.Domain.DomainModels;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing.Service.Implementation
{
    public class LocationService: ILocationService
    {
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public void CreateNewLocation(Location l)
        {
            this._locationRepository.Insert(l);
        }
        public Location GetDetailsForLocation(Guid? id)
        {
            return this._locationRepository.Get(id);
        }

        public void DeleteLocation(Guid id)
        {
            var location = this.GetDetailsForLocation(id);
            this._locationRepository.Delete(location);
        }

        public List<Location> GetAllLocations()
        {
            return this._locationRepository.GetAll().ToList();
        }

        public void UpdateExistingLocation(Location l)
        {
            this._locationRepository.Update(l);
        }
  
    }
}
