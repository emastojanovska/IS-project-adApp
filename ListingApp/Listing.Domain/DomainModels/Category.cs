﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Domain.DomainModels
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ListingPost> ListingPosts { get; set; }

        public Category(string Name)
        {
            this.Name = Name;
        }
    }
}
