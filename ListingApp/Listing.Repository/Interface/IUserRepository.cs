using Listing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<UserDetails> GetAll();
        UserDetails Get(string id);
        void Insert(UserDetails entity);
        void Update(UserDetails entity);
        void Delete(UserDetails entity);
    }
}
