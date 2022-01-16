using Listing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Service.Interface
{
    public interface IImageService
    {
        UserImage Get(Guid id);
        ListingImage GetListingImage(Guid id);


    }
}
