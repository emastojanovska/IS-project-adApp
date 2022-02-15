using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Domain.DomainModels
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ListingId { get; set; }
        public ListingPost Listing { get; set; }
        public string UserName { get; set; }
        public Comment() { }

    }
}
