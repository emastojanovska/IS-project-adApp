﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Domain.DomainModels
{
    public class ListingPost : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<ListingsInWishlist> ListingsInWishlists { get; set; }

        public virtual ICollection<Image> ListingImages { get; set; }

    }
}
