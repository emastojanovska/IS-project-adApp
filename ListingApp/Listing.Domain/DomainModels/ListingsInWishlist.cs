using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Domain.DomainModels
{
    public class ListingsInWishlist : BaseEntity
    {
        public Guid WishlistId { get; set; }
        public Guid ListingId { get; set; }
        public Wishlist Wishlist { get; set; }
        public ListingPost Listing { get; set; }

    }
}
