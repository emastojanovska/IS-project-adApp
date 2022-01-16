using Listing.Domain.DomainModels;
using Listing.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing.Repository.Implementation
{
    public class ListingRepository: IListingRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ListingPost> entities;
        string errorMessage = string.Empty;

        public ListingRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ListingPost>();
        }
        public IEnumerable<ListingPost> GetAll()
        {
            return entities.Include(z=>z.Category).Include(z=>z.Location).Include(z => z.ListingImages).AsEnumerable();
        }

        public ListingPost Get(Guid? id)
        {
            return entities.Include(z => z.Category).Include(z => z.Location).Include(z => z.ListingImages).SingleOrDefault(s => s.Id == id);

        }
        public void Insert(ListingPost entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(ListingPost entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(ListingPost entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<ListingPost> GetAllByLocationAndCategoryAndPrice(string location, string category, double price)
        {
            return entities
                .Where(x => x.Approved == true)
                .Where(x => x.Location.City.Contains(location))
                .Where(x=>x.Category.Name.Contains(category))
                .Where(x=>x.Price<=price)
                .Include(x => x.ListingImages)
                .Include(z => z.Category).Include(z => z.Location)
                .AsEnumerable();
        }

        public IEnumerable<ListingPost> GetAllActive()
        {
            IEnumerable<ListingPost> all = entities.Include(z => z.Category).Include(z => z.Location).Include(z => z.ListingImages).AsEnumerable();

            return all.Where(z => z.Approved).AsEnumerable();
        }

        public IEnumerable<ListingPost> GetAllInactive()
        {
            IEnumerable<ListingPost> all = entities.Include(z => z.Category).Include(z => z.Location).Include(z => z.ListingImages).AsEnumerable();

            return all.Where(z => !z.Approved).AsEnumerable();
        }

        public void Approve(Guid? id)
        {
            ListingPost listing = Get(id);
            listing.Approved = true;
            entities.Update(listing);
            context.SaveChanges();

        }
    }
}
