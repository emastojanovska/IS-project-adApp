using Listing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Repository.Interface
{
   public interface IListingRepository
    {
        IEnumerable<ListingPost> GetAll();
        ListingPost Get(Guid? id);
        void Insert(ListingPost entity);
        void Update(ListingPost entity);
        void Delete(ListingPost entity);
        IEnumerable<ListingPost> GetAllByLocationAndCategory(string location, string category);

    }
}
