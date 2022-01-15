using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListingsAdminApplication.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public byte ImageData { get; set; }
        public string MimeType { get; set; }

        public Guid ListingId { get; set; }
        public ListingPost Listing { get; set; }

    }
}
