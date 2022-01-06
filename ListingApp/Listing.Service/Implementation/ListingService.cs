using Listing.Domain.DomainModels;
using Listing.Domain.DTO;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Listing.Service.Implementation
{
    public class ListingService : IListingService
    {
        private readonly IRepository<ListingPost> _listingRepository;
        private readonly IRepository<ListingsInWishlist> _listingsInWishlistRepository;
        private readonly IUserRepository _userRepository;


        public ListingService(IRepository<ListingPost> listingRepository,
            IRepository<ListingsInWishlist> listingsInWishlistRepository,
            IUserRepository userRepository)
        {
            _listingRepository = listingRepository;
            _listingsInWishlistRepository = listingsInWishlistRepository;
            _userRepository = userRepository;
        }
        public bool AddToWishlist(AddToWishlistDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userWishlist = user.UserWishlist;

            if (item.ListingId != null && userWishlist != null)
            {
                var listing = this.GetDetailsForListing(item.ListingId);

                if (listing != null)
                {
                    ListingsInWishlist itemToAdd = new ListingsInWishlist
                    {
                        Id = Guid.NewGuid(),
                        Wishlist = userWishlist,
                        WishlistId = userWishlist.Id,
                        Listing = listing,
                        ListingId = listing.Id
                    };

                    this._listingsInWishlistRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewListing(ListingPost l)
        {
            this._listingRepository.Insert(l);
        }

        public void DeleteListing(Guid id)
        {
            var listing = this.GetDetailsForListing(id);
            this._listingRepository.Delete(listing);
        }

        public List<ListingPost> GetAllListings()
        {
            return this._listingRepository.GetAll().ToList();
        }

        public ListingPost GetDetailsForListing(Guid? id)
        {
            return this._listingRepository.Get(id);
        }

        public AddToWishlistDto GetWishlistInfo(Guid? id)
        {
            var listing = this._listingRepository.Get(id);
            AddToWishlistDto model = new AddToWishlistDto
            {
                SelectedListing = listing,
                ListingId = listing.Id
            };
            return model;
        }

        public void UpdeteExistingListing(ListingPost l)
        {
            this._listingRepository.Update(l);
        }
    }
}
