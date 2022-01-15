using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationListings.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminListingsController : ControllerBase
    {
        private readonly IListingService _listingService;

        public AdminListingsController(IListingService listingService)
        {
            this._listingService = listingService;
        }

        [HttpGet("[action]")]
        public List<ListingPost> GetListings()
        {
            return this._listingService.GetAllListings();
        }

        [HttpGet("[action]")]
        public List<ListingPost> GetInactiveListings()
        {
            return this._listingService.GetAllInactiveListings();
        }

        [HttpGet("[action]")]
        public List<ListingPost> GetActiveListings()
        {
            return this._listingService.GetAllActiveListings();
        }

        [HttpPost("[action]")]
        public ListingPost GetListingPostDetails(BaseEntity model)
        {
            return this._listingService.GetDetailsForListing(model.Id);
        }

        [HttpPost("[action]")]
        public ListingPost GetListing(BaseEntity model)
        {
            return _listingService.GetDetailsForListing(model.Id);
        }

        [HttpPost("[action]")]
        public bool ApproveListing(BaseEntity model)
        {
            try
            {
                _listingService.ApproveListing(model.Id);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

        }
    }
}
