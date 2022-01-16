using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Domain.DomainModels
{
    public class ListingImage: Image
    {
        public Guid ListingId { get; set; }
        public ListingPost Listing { get; set; }

        public ListingImage(byte[] data, string name, string type, string imageDataBase64, string imageSrc): base(data, name, type, imageDataBase64, imageSrc)
        {

        }
        public ListingImage()
        {

        }
    }
}
