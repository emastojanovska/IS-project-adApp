using Listing.Domain.DomainModels;
using Listing.Domain.DTO;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing.Service.Implementation
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IRepository<ListingsInWishlist> _listingsInWishlistRepository;
        private readonly IUserRepository _userRepository;


        public ListingService(IListingRepository listingRepository,
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
            l.DateCreated = DateTime.Now;
            l.DateUpdated = DateTime.Now;
            l.Approved = false;           
            this._listingRepository.Insert(l);
        }

        public void DeleteListing(Guid id)
        {
            var listing = this.GetDetailsForListing(id);
            this._listingRepository.Delete(listing);
        }

        public List<ListingPost> GetAllByLocationAndCategory(string location, string category)
        {
            if (category == "All")
            {
                category = "";
            }
            if (location == "All")
            {
                location = "";
            }
            return this._listingRepository.GetAllByLocationAndCategory(location, category).ToList();
        }

        public List<ListingPost> GetAllListings()
        {
            return this._listingRepository.GetAll().ToList();
        }

        public List<ListingPost> GetAllActiveListings()
        {
            return this._listingRepository.GetAllActive().ToList();
        }

        public List<ListingPost> GetAllInactiveListings()
        {
            return this._listingRepository.GetAllInactive().ToList();
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
            l.DateUpdated = DateTime.Now;
            this._listingRepository.Update(l);
        }

        public void ApproveListing(Guid? id)
        {
            this._listingRepository.Approve(id);
        }

        public List<ListingPost> GetAllListingsForUser(string id)
        {
            return GetAllListings().Where(z => z.UserId == id).ToList();
            /*.Where(z => z.UserId == id).*/
        }
    }
}
