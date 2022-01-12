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
            return entities.Include(z=>z.Category).Include(z=>z.Location).AsEnumerable();
        }

        public ListingPost Get(Guid? id)
        {
            return entities.Include(z => z.Category).Include(z => z.Location).SingleOrDefault(s => s.Id == id);
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

        public IEnumerable<ListingPost> GetAllByLocationAndCategory(string location, string category)
        {
            return entities.Where(x => x.Location.City.Contains(location))
                .Where(x=>x.Category.Name.Contains(category))
                .Include(z => z.Category).Include(z => z.Location)
                .AsEnumerable();
        }
    }
}
