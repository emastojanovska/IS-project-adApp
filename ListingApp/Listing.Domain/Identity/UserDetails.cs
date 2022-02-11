using Listing.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Listing.Domain.Identity
{
    public class UserDetails : IdentityUser
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public Guid ImageId { get; set; }
        public virtual UserImage Image { get; set; }
        public virtual Wishlist UserWishlist { get; set; }
        public virtual ICollection<Message> UserMessages { get; set; }
        
        public UserDetails()
        {
            this.UserMessages = new List<Message>();
        }
      
    }
}
