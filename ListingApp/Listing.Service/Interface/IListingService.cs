using Listing.Domain.DomainModels;
using Listing.Domain.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Service.Interface
{
    public interface IListingService
    {
        List<ListingPost> GetAllListings();
        ListingPost GetDetailsForListing(Guid? id);
        void ValidateListing(Guid? id, string action);
        void CreateNewListing(ListingPost l, string userId, List<IFormFile> images);
        void UpdeteExistingListing(ListingPost l, List<IFormFile> images);
        AddToWishlistDto GetWishlistInfo(Guid? id);
        void DeleteListing(Guid id);
        bool AddToWishlist(AddToWishlistDto item, string userID);
        List<ListingPost> GetAllByLocationAndCategoryAndPrice(string location, string category, double price);
        List<ListingPost> GetAllApprovedListings();
        List<ListingPost> GetAllDisapprovedListings();
        List<ListingPost> GetAllInactiveListings();
        List<ListingPost> GetAllListingsForUser(string id);
        void UpdeteExistingListing(ListingPost listing);
        List <ListingPost> GetAllByDate(DateTime date);
        List<ListingPost> GetAllByTitleOrDescription(string search);

        void AddCommentToListing(ListingPost listingPost, string userId, string text);
    }
}
