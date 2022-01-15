using Listing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Domain.DTO
{
    public class WishlistDto
    {
        public List<ListingsInWishlist> Listings { get; set; }

    }
}
