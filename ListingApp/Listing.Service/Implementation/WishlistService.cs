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
    public class WishlistService : IWishlistService
    {
        private readonly IRepository<Wishlist> _wishlistRepositorty;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ListingsInWishlist> _listingsInWishlistRepository;


        public WishlistService(IRepository<Wishlist> wishlistRepositorty,
             IUserRepository userRepository,
             IRepository<ListingsInWishlist> listingsInWishlistRepository)
        {
            _wishlistRepositorty = wishlistRepositorty;
            _userRepository = userRepository;
            _listingsInWishlistRepository = listingsInWishlistRepository;
        }

        public bool checkIfExist(string userId, Guid id)
        {

            if (!string.IsNullOrEmpty(userId) && id != null)
            {

                var loggedInUser = _userRepository.Get(userId);

                var userWishlist = loggedInUser.UserWishlist;

                if(userWishlist.ListingsInWishlists.Where(z => z.ListingId.Equals(id)).FirstOrDefault() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }

            return false;
        }

        public bool deleteListingFromWishlist(string userId, Guid id)
        {

            if (!string.IsNullOrEmpty(userId) && id != null)
            {       

                var loggedInUser = this._userRepository.Get(userId);

                var userWishlist = loggedInUser.UserWishlist;

                var itemToDelete = userWishlist.ListingsInWishlists.Where(z => z.ListingId.Equals(id)).FirstOrDefault();

                userWishlist.ListingsInWishlists.Remove(itemToDelete);

                _wishlistRepositorty.Update(userWishlist);

                return true;
            }

            return false;
        }

        public WishlistDto getWishlistInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userWishlist = loggedInUser.UserWishlist;

            var AllListings = userWishlist.ListingsInWishlists.ToList();

            WishlistDto wlDto = new WishlistDto
            {
                Listings = AllListings
            };           


            return wlDto;
        }
    }
}
