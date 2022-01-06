using Listing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Domain.DomainModels
{
    public class Location : BaseEntity
    {
        public string City { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
        public UserDetails User { get; set; }

    }
}
