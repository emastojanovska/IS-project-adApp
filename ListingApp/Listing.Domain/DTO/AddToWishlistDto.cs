using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listing.Domain.DomainModels;

namespace Listing.Domain.DTO
{
    public class AddToWishlistDto
    {
        public DomainModels.ListingPost SelectedListing { get; set; }
        public Guid ListingId { get; set; }

    
    }
}
