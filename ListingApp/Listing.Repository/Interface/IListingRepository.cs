using Listing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Repository.Interface
{
   public interface IListingRepository
    {
        IEnumerable<ListingPost> GetAll();
        IEnumerable<ListingPost> GetAllApproved();
        IEnumerable<ListingPost> GetAllDisapproved();
        IEnumerable<ListingPost> GetAllInactive();
        ListingPost Get(Guid? id);
        void Insert(ListingPost entity);
        void Update(ListingPost entity);
        void Delete(ListingPost entity);
        IEnumerable<ListingPost> GetAllByLocationAndCategoryAndPrice(string location, string category, double price);
        IEnumerable<ListingPost> GetAllByDate(DateTime date);
        IEnumerable<ListingPost> GetAllByTitleOrDescription(string search);

    }
}
