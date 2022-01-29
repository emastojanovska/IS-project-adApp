using Listing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Repository.Interface
{
   public interface IListingRepository
    {
        IEnumerable<ListingPost> GetAll();
        IEnumerable<ListingPost> GetAllActive();
        IEnumerable<ListingPost> GetAllInactive();

        ListingPost Get(Guid? id);
        void Insert(ListingPost entity);
        void Update(ListingPost entity);
        void Delete(ListingPost entity);
        void Validate(Guid? id, string action);
        IEnumerable<ListingPost> GetAllByLocationAndCategoryAndPrice(string location, string category, double price);

    }
}
