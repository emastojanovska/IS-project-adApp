using Listing.Domain.DomainModels;
using Listing.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Service.Interface
{
    public interface IListingService
    {
        List<ListingPost> GetAllListings();
        ListingPost GetDetailsForListing(Guid? id);
        void CreateNewListing(ListingPost l);
        void UpdeteExistingListing(ListingPost l);
        AddToWishlistDto GetWishlistInfo(Guid? id);
        void DeleteListing(Guid id);
        bool AddToWishlist(AddToWishlistDto item, string userID);
    }
}
