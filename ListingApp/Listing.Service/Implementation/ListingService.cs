  using Listing.Domain.DomainModels;
using Listing.Domain.DTO;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Listing.Service.Implementation
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IRepository<ListingsInWishlist> _listingsInWishlistRepository;
        private readonly IUserRepository _userRepository;


        public ListingService(IListingRepository listingRepository,
            IRepository<ListingsInWishlist> listingsInWishlistRepository,
            IUserRepository userRepository)
        {
            _listingRepository = listingRepository;
            _listingsInWishlistRepository = listingsInWishlistRepository;
            _userRepository = userRepository;
        }

        public bool AddToWishlist(AddToWishlistDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userWishlist = user.UserWishlist;

            if (item.ListingId != null && userWishlist != null)
            {
                var listing = this.GetDetailsForListing(item.ListingId);

                if (listing != null)
                {
                    ListingsInWishlist itemToAdd = new ListingsInWishlist
                    {
                        Id = Guid.NewGuid(),
                        Wishlist = userWishlist,
                        WishlistId = userWishlist.Id,
                        Listing = listing,
                        ListingId = listing.Id
                    };

                    this._listingsInWishlistRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewListing(ListingPost l, string userId, List<IFormFile> images)
        {
            foreach (var image in images)
            {
                var data = GetByteArrayFromImage(image);
                var imageDataBase64 = Convert.ToBase64String(data, 0, data.Length);
                var imageSrc = "data:image/png;base64," + imageDataBase64;

                ListingImage ListingImage = new ListingImage(data, Path.GetFileName(image.FileName), image.ContentType, imageDataBase64, imageSrc);

                l.ListingImages.Add(ListingImage);
            }
            l.DateCreated = DateTime.Now;
            l.DateUpdated = DateTime.Now;
            l.Status = "undefined";
            l.UserId = userId;

            this._listingRepository.Insert(l);

        }
        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        public void DeleteListing(Guid id)
        {
            var listing = this.GetDetailsForListing(id);
            this._listingRepository.Delete(listing);
        }

        public List<ListingPost> GetAllByLocationAndCategoryAndPrice(string location, string category, double price)
        {
            if (category == "All")
            {
                category = "";
            }
            if (location == "All")
            {
                location = "";
            }
            return this._listingRepository.GetAllByLocationAndCategoryAndPrice(location, category, price).ToList();
        }

        public List<ListingPost> GetAllListings()
        {
            return this._listingRepository.GetAll().ToList();
        }

        public List<ListingPost> GetAllApprovedListings()
        {
            return this._listingRepository.GetAllApproved().ToList();
        }
        public List<ListingPost> GetAllDisapprovedListings()
        {
            return this._listingRepository.GetAllDisapproved().ToList();
        }

        public List<ListingPost> GetAllInactiveListings()
        {
            return this._listingRepository.GetAllInactive().ToList();
        }

        public ListingPost GetDetailsForListing(Guid? id)
        {
            return this._listingRepository.Get(id);
        }

        public AddToWishlistDto GetWishlistInfo(Guid? id)
        {
            var listing = this._listingRepository.Get(id);
            AddToWishlistDto model = new AddToWishlistDto
            {
                SelectedListing = listing,
                ListingId = listing.Id
            };
            return model;
        }

        public void UpdeteExistingListing(ListingPost l, List<IFormFile> images)
        {
            foreach (var image in images)
            {
                var data = GetByteArrayFromImage(image);
                var imageDataBase64 = Convert.ToBase64String(data, 0, data.Length);
                var imageSrc = "data:image/png;base64," + imageDataBase64;

                ListingImage ListingImage = new ListingImage(data, Path.GetFileName(image.FileName), image.ContentType, imageDataBase64, imageSrc);

                l.ListingImages.Add(ListingImage);
            }
            l.DateUpdated = DateTime.Now;
            this._listingRepository.Update(l);
        }
        public void UpdeteExistingListing(ListingPost listing)
        {
            listing.DateUpdated = DateTime.Now;
            this._listingRepository.Update(listing);
        }

        public void ValidateListing(Guid? id, string action)
        {
            ListingPost listing = GetDetailsForListing(id);
            listing.Status = action;
            this._listingRepository.Update(listing);
        }

        public List<ListingPost> GetAllListingsForUser(string id)
        {
            return GetAllListings().Where(z => z.UserId == id).ToList();
        }

        public List<ListingPost> GetAllByDate(DateTime date)
        {
            if(date.Year==1)
            {
                return this._listingRepository.GetAllApproved().ToList();
            }
            return this._listingRepository.GetAllByDate(date).ToList();
        }

        public List<ListingPost> GetAllByTitleOrDescription(string search)
        {
            if (search == null) search = "";
            return this._listingRepository.GetAllByTitleOrDescription(search).ToList();
        }

        public void AddCommentToListing(ListingPost listingPost, string userId, string text)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.DateCreated = DateTime.Now;
            comment.UserId = userId;
            var user = _userRepository.Get(userId);
            comment.UserName = user.FirstName + " " + user.LastName;
            listingPost.Comments.Add(comment);
            UpdeteExistingListing(listingPost);
        }
    }
}
