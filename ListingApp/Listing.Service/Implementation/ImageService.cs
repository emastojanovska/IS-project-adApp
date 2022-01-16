using Listing.Domain.DomainModels;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Service.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IRepository<UserImage> _userImageRepository;
        private readonly IRepository<ListingImage> _listingImageRepository;


        public ImageService(IRepository<UserImage> userImageRepository, IRepository<ListingImage> listingImageRepository)
        {
             _userImageRepository = userImageRepository;
            _listingImageRepository = listingImageRepository;
        }

        public UserImage Get(Guid id)
        {
            return _userImageRepository.Get(id);
        }

        public ListingImage GetListingImage(Guid id)
        {
            return _listingImageRepository.Get(id);
        }
    }
}
