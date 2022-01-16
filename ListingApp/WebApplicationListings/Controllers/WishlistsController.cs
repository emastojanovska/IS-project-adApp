using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationListings.Controllers
{
    [Authorize]
    public class WishlistsController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly IListingService _listingService;

        public WishlistsController(IWishlistService wishlistService, IListingService listingService)
        {
            _wishlistService = wishlistService;
            _listingService = listingService;

        }
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var allListings = _wishlistService.getWishlistInfo(userId);

            allListings.Listings.ForEach(z => z.Listing = _listingService.GetDetailsForListing(z.ListingId));

            return View(allListings);
        }

        public IActionResult DeleteFromWishlist(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _wishlistService.deleteListingFromWishlist(userId, id);
            
            return RedirectToAction("Index", "Wishlists");           
           
        }
    }
}
