using Listing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Domain.DomainModels
{
    public class Wishlist : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual UserDetails Owner { get; set; }
        public virtual ICollection<ListingsInWishlist> ListingsInWishlists { get; set; }
    }
}

