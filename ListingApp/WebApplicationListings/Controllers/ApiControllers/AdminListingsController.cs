using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationListings.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminListingsController : ControllerBase
    {
        private readonly IListingService _listingService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public AdminListingsController(IListingService listingService, IEmailService emailService, IUserService userService)
        {
            this._listingService = listingService;
            this._emailService = emailService;
            this._userService = userService;
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
        public async Task<bool> ApproveListing(ValidateEntity model)
        {
            try
            {
                _listingService.ValidateListing(model.Id, model.Action);
                ListingPost listing = _listingService.GetDetailsForListing(model.Id);
                string userEmail = _userService.Get(listing.UserId).Email;
                await _emailService.SendEmailAsync(listing, model.Action, userEmail);
                if(model.Action=="disapproved")
                {
                    _listingService.DeleteListing(model.Id);
                }
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

        }
    }
}
