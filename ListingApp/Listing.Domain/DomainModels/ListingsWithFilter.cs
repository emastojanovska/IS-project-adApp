using System;
using System.Collections.Generic;
using System.Text;

namespace Listing.Domain.DomainModels
{
    public class ListingsWithFilter
    {

        public ICollection<ListingPost> ListingPosts { get; set; }
        public string SelectedCategory { get; set; }
        public string SelectedLocation { get; set; }
        public double Price { get; set; }

        public ListingsWithFilter(List<ListingPost> listings, string selectedCategory, string selectedLocation, double Price)
        {
            this.ListingPosts = listings;
            this.SelectedCategory = selectedCategory;
            this.SelectedLocation = selectedLocation;
            this.Price = Price;

        }
        public ListingsWithFilter()
        {
            this.ListingPosts = new List<ListingPost>();
        }
        public ListingsWithFilter(string selectedCategory, string selectedLocation, double Price)
        {
            this.ListingPosts = new List<ListingPost>();
            this.SelectedCategory = selectedCategory;
            this.SelectedLocation = selectedLocation;
            this.Price = Price;
        }

    }
}
