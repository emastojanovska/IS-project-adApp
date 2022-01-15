using Listing.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Service.Interface
{
    public interface IWishlistService
    {
        WishlistDto getWishlistInfo(string userId);
        bool deleteListingFromWishlist(string userId, Guid id);

        bool checkIfExist(string userId, Guid id);

    }
}
