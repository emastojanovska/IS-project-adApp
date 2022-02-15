using Listing.Domain.DomainModels;
using Listing.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing.Repository.Implementation
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ListingPost> entities;

        public ListingRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ListingPost>();
        }
        public IEnumerable<ListingPost> GetAll()
        {
            return entities.Include(z => z.Category)
                .Include(z => z.Location)
                .Include(z => z.ListingImages)
                .Include(z => z.Comments).AsEnumerable();
        }

        public ListingPost Get(Guid? id)
        {
            return entities.Include(z => z.Category)
                .Include(z => z.Location)
                .Include(z => z.ListingImages)
                .Include(z => z.Comments)
                .SingleOrDefault(s => s.Id == id);

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
                .Where(x => x.Status == "approved")
                .Where(x => x.Location.City.Contains(location))
                .Where(x => x.Category.Name.Contains(category))
                .Where(x => x.Price <= price)
                .Include(x => x.ListingImages)
                .Include(z => z.Category)
                .Include(z => z.Location)
                .AsEnumerable();
        }

        public IEnumerable<ListingPost> GetAllByDate(DateTime date)
        {
            DateTime dateSubstracted = date.AddDays(-7);
            return entities
                .Where(x => x.Status == "approved")
                .Where(x => x.DateCreated >= dateSubstracted)
                .Include(x => x.ListingImages)
                .Include(z => z.Category)
                .Include(z => z.Location)
                .AsEnumerable();
        }
        public IEnumerable<ListingPost> GetAllApproved()
        {
            IEnumerable<ListingPost> all = entities.Include(z => z.Category).Include(z => z.Location).Include(z => z.ListingImages).Include(z => z.Comments).AsEnumerable();

            return all.Where(z => z.Status == "approved").AsEnumerable();
        }
        public IEnumerable<ListingPost> GetAllDisapproved()
        {
            IEnumerable<ListingPost> all = entities.Include(z => z.Category).Include(z => z.Location).Include(z => z.ListingImages).AsEnumerable();

            return all.Where(z => z.Status == "disapproved").AsEnumerable();
        }

        public IEnumerable<ListingPost> GetAllInactive()
        {
            IEnumerable<ListingPost> all = entities.Include(z => z.Category).Include(z => z.Location).Include(z => z.ListingImages).Include(z => z.Comments).AsEnumerable();

            return all.Where(z => z.Status == "undefined").AsEnumerable();
        }

        public IEnumerable<ListingPost> GetAllByTitleOrDescription(string search)
        {
            return entities
                 .Where(x => x.Status == "approved")
                 .Where(x => x.Title.ToLower().Contains(search.ToLower()) 
                 || x.Description.ToLower().Contains(search.ToLower()))
                 .Include(x => x.ListingImages)
                 .Include(z => z.Category)
                 .Include(z => z.Location)
                 .AsEnumerable();
        }
    }
}
