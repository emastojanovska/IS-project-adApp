using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Domain.DTO
{
    public class AddCommentToListingPost
    {
        public DomainModels.ListingPost SelectedListing { get; set; }
        public Guid ListingId { get; set; }
        public string Comment { get; set; }
    }
}
