using Listing.Domain.Identity;
using Listing.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Listing.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<UserDetails> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<UserDetails>();
        }
        public IEnumerable<UserDetails> GetAll()
        {
            return entities.AsEnumerable();
        }

        public UserDetails Get(string id)
        {
            return entities
               .Include(z => z.UserWishlist)
               .Include("UserWishlist.ListingsInWishlists")
               .Include("UserWishlist.ListingsInWishlists.Wishlist")
               .Include(z=>z.Image)
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(UserDetails entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(UserDetails entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(UserDetails entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
