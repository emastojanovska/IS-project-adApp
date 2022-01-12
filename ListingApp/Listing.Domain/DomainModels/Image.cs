using Listing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Domain.DomainModels
{
    public class Image : BaseEntity
    {
        public byte ImageData { get; set; }
        public string MimeType { get; set; }

        public Guid ListingId { get; set; }
        public ListingPost Listing { get; set; }

        public virtual UserDetails UserImage { get; set; }
    }
}
